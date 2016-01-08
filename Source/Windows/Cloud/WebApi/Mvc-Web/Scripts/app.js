var baseBackendUrl = 'http://localhost:8090/',
    privateOwnerId = '3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C',
    urls = {
        loginUrl: baseBackendUrl + '/oauth/token',
        storesUrl: baseBackendUrl + '/api/gateway/stock/stocks/?privateOwnerId=' + privateOwnerId,
        productsUrl: baseBackendUrl + '/api/gateway/stock/stockproduct/stockid/?privateOwnerId=' + privateOwnerId,
        stockProductsUrl: baseBackendUrl + '/api/gateway/stock/stockproduct/stockid/',
        saveStockProduct: baseBackendUrl + '/api/gateway/stock/stockproduct/save/?privateOwnerId=' + privateOwnerId,
        reorderCalcTypeUrl: baseBackendUrl + "/api/gateway/basedata/reordercalctypes"
    },
    gridAuthHeader = function (req) {
        var tokenKey = 'accessToken',
            token = $.cookie("token");
        req.setRequestHeader('Authorization', 'Bearer ' + token);
    }

toastr.options = {
    "closeButton": false,
    "debug": false,
    "newestOnTop": true,
    "progressBar": true,
    "positionClass": "toast-bottom-left",
    "preventDuplicates": true,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
};
showError = function (title, message) {
    toastr["error"](title, message);
};
//******************************************************************//
function headerMenuViewModel() {
    var self = this;
    self.shouldShowLogout = ko.observable(false);

};
var headerMenu = new headerMenuViewModel();
//******************************************************************//
function accountManagerViewModel() {
    var self = this;

    var tokenKey = 'accessToken';

    self.result = ko.observable();
    self.user = ko.observable();
    self.loginEmail = ko.observable();
    self.loginPassword = ko.observable();
    self.requestAppObject = ko.observable();

    function showAjaxError(jqXHR) {
        showError(jqXHR.responseJSON.error, jqXHR.responseJSON.error_description);

        self.result(jqXHR.responseJSON.error + ': ' + jqXHR.responseJSON.error_description);

        console.log(jqXHR.status + ': ' + jqXHR.statusText);
    }

    self.callApi = function (url, callType, callBackFunc) {
        self.result('');

        var token = $.cookie("token"),
            headers = {};

        if (token)
            headers.Authorization = 'Bearer ' + token;
        else {
            self.requestAppObject({ url: url, callType: callType, callBackFunc: callBackFunc });
            self.openLogin();

            return;
        }

        headerMenu.shouldShowLogout(true);

        $.ajax({
            type: callType,
            url: url,
            headers: headers
        }).done(function (data) {
            self.result(data);
            callBackFunc(data);
        }).fail(showAjaxError);
    }

    var $loginForm = $(".login-form");
    self.login = function () {
        self.result('');

        if (self.loginEmail() == undefined || self.loginEmail() == '' || self.loginPassword() == undefined || self.loginPassword() == '') {
            self.result('لطفا اطلاعات کاربری خود را وارد نمایید');
            return;
        }

        var loginData = {
            grant_type: 'password',
            username: self.loginEmail(),
            password: self.loginPassword()
        };

        $.ajax({
            type: 'POST',
            url: urls.loginUrl,
            data: loginData,
        }).done(function (data) {
            self.user(data.userName);

            $.cookie("token", data.access_token, { expires: 7 });
            $loginForm.data("kendoWindow").close();

            retUrlObject = self.requestAppObject();
            if (retUrlObject)
                self.callApi(retUrlObject.url, retUrlObject.callType, retUrlObject.callBackFunc);

            headerMenu.shouldShowLogout(true);

        }).fail(showAjaxError);
    }

    self.logout = function () {
        self.user('');
        headerMenu.shouldShowLogout(false);
        $.removeCookie('token');
        window.location = '/';
    }

    self.openLogin = function () {
        $loginForm.data("kendoWindow").center();
        $loginForm.data("kendoWindow").open();
    }

    $(document).on("click", ".btn-login", function () {
        self.login();
    });

    self.initLoginWindow = function () {
        $loginForm.removeClass('hide');
        if (!$loginForm.data("kendoWindow")) {
            $loginForm.kendoWindow({
                width: "450px",
                title: "ورود",
                actions: [],
                modal: true,
                visible: false,
                resizable: false
            });
        }
    };

    self.initLoginWindow();
};
var accountManagerApp = new accountManagerViewModel();
//******************************************************************//
function productManagerViewModel() {
    // Data
    var self = this;

    self.chosenStore = ko.observable();
    self.stores = ko.observableArray([]);
    self.products = ko.observableArray([]);
    self.flg_initGrid = ko.observable(true);

    self.refreshStores = function () {
        accountManagerApp.callApi(urls.storesUrl, 'GET', function (data) {
            self.stores(data);
            if (self.stores().length > 0) {
                self.chosenStore(self.stores()[0]);

                if (self.flg_initGrid()) {
                    self.flg_initGrid(false);
                    self.initGrid(self.chosenStore().Id);
                }
                else
                    self.refreshProducts(self.chosenStore());
            }
        });
    };

    self.refreshProducts = function (data) {
        self.chosenStore(data);
        $('.products-grid').data("kendoGrid").dataSource.read();
    };

    self.initGrid = function (id) {
        dataSource = new kendo.data.DataSource({
            transport: {
                read: {
                    url: urls.stockProductsUrl,
                    dataType: "json",
                    contentType: "application/json",
                    type: "POST",
                    data: { privateOwnerId: privateOwnerId, stockId: self.chosenStore().uniqueId },
                    beforeSend: gridAuthHeader
                },
                update: {
                    url: urls.saveStockProduct,
                    type: "POST",
                    contentType: "application/json",
                    dataType: "json",
                    beforeSend: gridAuthHeader
                },
                parameterMap: function (options, operation) {
                    options.stockId = self.chosenStore().uniqueId;
                    if (operation == "read")
                        return kendo.stringify(options);

                    if (operation !== "read" && options.models)
                        return kendo.stringify(options.models);
                }
            },
            batch: true,
            pageSize: 20,
            schema: {
                model: {
                    id: "productGuid",
                    fields: {
                        productGuid: { editable: false, nullable: true },
                        productCode: { editable: false },
                        productName: { editable: false },
                        QtyPerPack: { editable: false },
                        minQty: { type: "number", validation: { min: 0 } },
                        maxQty: { type: "number", validation: { min: 0 } },
                        reorderLevel: { type: "number", validation: { min: 0 } },
                        reorderCalcTypeInfo: { defaultValue: { uniqueId: "", reorderTypeName: "" } }

                    }
                }
            }
        });

        $(".products-grid").kendoGrid({
            dataSource: dataSource,
            navigatable: true,
            pageable: true,
            height: 550,
            toolbar: ["save", "cancel"],//"create",
            columns: [
                { field: "productCode", title: "کد کالا", width: 50 },
                { field: "productName", title: "نام کالا", width: 50 },
                { field: "minQty", title: "حداقل موجودی", width: 50 },
                { field: "maxQty", title: "حداکثر موجودی", width: 50 },
                { field: "reorderLevel", title: "نقطه سفارش", width: 50 },
                { field: "reorderCalcTypeInfo", title: "شیوه سفارش", width: "180px", editor: categoryDropDownEditor, template: "#=reorderCalcTypeInfo.reorderTypeName#" },
            ],
            editable: true
        });

        function categoryDropDownEditor(container, options) {
            $('<input required data-text-field="reorderTypeName" data-value-field="uniqueId" data-bind="value:' + options.field + '"/>')
                .appendTo(container)
                .kendoDropDownList({
                    dataSource: {
                        transport: {
                            read: {
                                url: urls.reorderCalcTypeUrl,
                                dataType: "json",
                                contentType: "application/json",
                                type: "POST",
                                data: { privateOwnerId: privateOwnerId },
                                beforeSend: gridAuthHeader
                            },
                            parameterMap: function (options, operation) {
                                if (operation == "read")
                                    return kendo.stringify(options);
                            }
                        }
                    }
                });
        }
    };
    self.refreshStores();
};
