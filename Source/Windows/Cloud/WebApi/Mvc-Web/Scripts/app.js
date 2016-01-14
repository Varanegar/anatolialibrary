var baseBackendUrl = 'http://localhost:59822',
    privateOwnerId = '3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C',
    urls = {
        loginUrl: baseBackendUrl + '/oauth/token',
        storesUrl: baseBackendUrl + '/api/gateway/stock/stocks/?privateOwnerId=' + privateOwnerId,
        userStocksUrl: baseBackendUrl + '/api/gateway/stock/userStocks/?privateOwnerId=' + privateOwnerId,
        productsUrl: baseBackendUrl + '/api/gateway/stock/stockproduct/stockid/?privateOwnerId=' + privateOwnerId,
        stockProductsUrl: baseBackendUrl + '/api/gateway/stock/stockproduct/stockid/',
        saveStockProduct: baseBackendUrl + '/api/gateway/stock/stockproduct/save/?privateOwnerId=' + privateOwnerId,
        reorderCalcTypeUrl: baseBackendUrl + "/api/gateway/basedata/reordercalctypes",
        usersUrl: baseBackendUrl + "/api/accounts/users",
        stockPageUrl: "/Products/",
    },
    errorMessage =
    {
        unAuthorized: "شما مجوز لازم را برای این درخواست ندارید!",
    },
gridAuthHeader = function (req) {
    var tokenKey = 'accessToken',
        token = $.cookie("token");
    req.setRequestHeader('Authorization', 'Bearer ' + token);
};

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
showSuccess = function (title, message) {
    toastr["success"](title, message);
};

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}
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
        var title = jqXHR.status,
            message = jqXHR.statusText

        if (jqXHR.responseJSON) {
            title = jqXHR.responseJSON.error;
            message = jqXHR.responseJSON.error_description;
        }

        if (jqXHR.status == 401) {
            title = '401';
            message = errorMessage.unAuthorized;
        }
        showError(title, message);

        console.log(title + ': ' + message);
    }

    self.callApi = function (url, callType, callBackFunc) {
        self.result('');

        var token = $.cookie("token"),
            headers = { ownerKey: privateOwnerId };

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
        }).fail(function (jqXHR) {
            showAjaxError(jqXHR);

            if (jqXHR.status == 401) {
                self.requestAppObject({ url: url, callType: callType, callBackFunc: callBackFunc });
                self.openLogin();
            }
        });
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
                //find query string stock id in store list
                var stockId = getParameterByName('stockId');
                if (stockId && stockId !== '') {
                    var match = ko.utils.arrayFirst(self.stores(), function (item) {
                        return item.uniqueId === stockId;
                    });
                    if (match && match != null)
                        self.chosenStore(match);
                    else
                        self.chosenStore(self.stores()[0]);
                }
                else
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
                { field: "productCode", title: "کد کالا", width: 100 },
                { field: "productName", title: "نام کالا", width: 200 },
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

    self.testAuth = function () {
        accountManagerApp.callApi(baseBackendUrl + '/api/TestAuth/getsample', 'POST', function (data) {
            debugger
        });
    }
    self.testAuth();
};
//******************************************************************//

//Todo: this view model should be complete.
var fakeBackendUrl = "/api/ProductManager/",
    fakeUrls = {
        storeRequestsUrl: fakeBackendUrl + "GetStoreRequests",
        requestReviewUrl: fakeBackendUrl + 'GetRequestReview',
        updateRequestReviewUrl: fakeBackendUrl + 'UpdateRequestReview',
        requestReviewDetailsUrl: fakeBackendUrl + 'GetRequestReviewDetail'
    };

function ReviewProductRequestViewModel() {
    // Data
    var self = this;

    self.stockRequests = ko.observableArray([]);

    self.chosenStoreRequest = ko.observable();
    self.storeRequestInfo = ko.observable();
    self.shouldShowRequestInfo = ko.observable(false);

    self.StoreName = ko.observable();
    self.RequestDate = ko.observable();
    self.RequestNumber = ko.observable();
    self.State = ko.observable();

    self.refreshStockRequest = function (term) {
        //todo: change this code with accountManagerApp call api method.
        $.post(fakeUrls.storeRequestsUrl, { term: term }, function (data, textStatus, jqXHR) {
            self.stockRequests(data);
            if (self.stockRequests().length > 0)
                self.refreshStoreRequestInfo(self.stockRequests()[0]);
        }, "json");
    };

    self.refreshStoreRequestInfo = function (data) {
        self.chosenStoreRequest(data);
        self.initRequestReviewGrid();
        self.shouldShowRequestInfo(true);
    };

    self.initRequestReviewGrid = function () {
        dataSource = new kendo.data.DataSource({
            transport: {
                read: {
                    url: fakeUrls.requestReviewUrl,
                    dataType: "json",
                    contentType: "application/json",
                    type: "POST",
                    data: { StoreRequestId: self.chosenStoreRequest().Id }
                },
                update: {
                    url: fakeUrls.updateRequestReviewUrl,
                    type: "POST",
                    contentType: "application/json",
                    dataType: "json"
                },
                parameterMap: function (options, operation) {
                    options.StoreRequestId = self.chosenStoreRequest().Id;
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
                    id: "Id",
                    fields: {
                        Id: { editable: false, nullable: true },
                        ModifiedBy: { type: "string", editable: false },
                        ModifiedDate: { type: "string", editable: false },
                        ModifiedLargeUnit: { type: "number", editable: false },
                        ModifiedSmallUnit: { type: "number", editable: false },
                        LargeUnit: { type: "number", validation: { required: true, min: 1 } },
                        SmallUnit: { type: "number", validation: { required: true, min: 1 } }
                    }
                }
            }
        });

        $(".request-review-grid").kendoGrid({
            dataSource: dataSource,
            navigatable: true,
            pageable: true,
            sortable: true,
            height: 550,
            detailTemplate: kendo.template($("#template").html()),
            detailInit: detailInit,
            dataBound: function () {
                this.expandRow(this.tbody.find("tr.k-master-row").first());
            },
            toolbar: ["save"],
            columns: [
                { field: "ModifiedBy", title: "نام کاربر" },
                { field: "ModifiedDate", title: "تاریخ تغییر" },
                { field: "ModifiedLargeUnit", title: "تعداد واحد بزرگ", width: 80 },
                { field: "ModifiedSmallUnit", title: "تعداد واحد کوچک", width: 80 },
                { field: "LargeUnit", title: "تعداد واحد بزرگ", width: 80 },
                { field: "SmallUnit", title: "تعداد واحد کوچک", width: 80 },
            ],
            editable: true
        });
    };

    function detailInit(e) {
        var detailRow = e.detailRow;

        detailRow.find(".request-details-grid").kendoGrid({
            dataSource: {
                transport: {
                    read: {
                        url: fakeUrls.requestReviewDetailsUrl,
                        dataType: "json",
                        contentType: "application/json",
                        type: "POST",
                        data: { RequestReviewId: e.data.Id }
                    },
                    parameterMap: function (options, operation) {
                        options.RequestReviewId = e.data.Id;
                        if (operation == "read")
                            return kendo.stringify(options);
                    }
                },
                pageSize: 10
            },
            scrollable: false,
            sortable: true,
            pageable: true,
            columns: [
                { field: "ProductName", title: "نام محصول", width: "70px" },
                { field: "Number", title: "تعداد", width: "110px" },
            ]
        });
    }

    self.refreshStockRequest();

    $(document).on("keypress", ".stock-term", function (e) {
        var term = $(this).val();
        if (e.which == 13)
            self.refreshStockRequest(term);
    });

    $(document).on("click", ".btn-lnk-stock", function (e) {
        e.preventDefault();

        var id = $(this).attr('data-id');

        window.location = urls.stockPageUrl + "?stockId=" + id;
    });
};
//******************************************************************//

//Todo: this view model should be complete.
var storesUrl = '/api/ProductManager/GetStores';
var productsUrl = '/api/ProductManager/GetProducts';

function ProductHistoryManagerViewModel() {
    // Data
    var self = this;
    self.chosenStore = ko.observable();
    self.chosenProduct = ko.observable();

    self.stores = ko.observableArray([]);
    self.products = ko.observableArray([]);

    self.chosenStore.subscribe(function (newValue) {
        self.refreshProducts(newValue);
        $(".product-term").removeAttr('disabled');
    }, self);

    self.refreshStores = function () {
        accountManagerApp.callApi(storesUrl, 'GET', function (data) {
            self.stores(data);
        });
    };

    self.refreshProducts = function (data, term) {

        $.post(productsUrl, { StoreId: data.Id, term: term }, self.products)
        .then(function () {
            if (self.products().length > 0)
                self.refreshHistory(self.products()[0], true);
        },
        function (ex) {
            console.log(ex.message);
        });
    };

    self.refreshHistory = function (data, initGrid_flag) {
        self.chosenProduct(data);

        if (initGrid_flag)
            self.initGrid(self.chosenStore().Id);
        else {
            $('.history-grid').data("kendoGrid").dataSource.read();
            $('.history-grid').data("kendoGrid").refresh();
        }
    };

    self.initGrid = function (id) {
        var crudServiceBaseUrl = "/api/ProductManager/",
        dataSource = new kendo.data.DataSource({
            transport: {
                read: {
                    url: crudServiceBaseUrl + "GetProductRequestsHistory",
                    dataType: "json",
                    contentType: "application/json",
                    type: "POST",
                    data: { ProductId: self.chosenProduct().Id }
                },
                parameterMap: function (options, operation) {
                    options.ProductId = self.chosenProduct().Id;
                    if (operation == "read")
                        return kendo.stringify(options);
                }
            },
            batch: true,
            pageSize: 20,
            schema: {
                model: {
                    id: "Id",
                    fields: {
                        Id: { editable: false, nullable: true },
                        ModifiedBy: { type: "string" },
                        ModifiedDate: { type: "string" },
                        LargeUnit: { type: "number" },
                        SmallUnit: { type: "number" }
                    }
                }
            }
        });

        $(".history-grid").kendoGrid({
            dataSource: dataSource,
            navigatable: true,
            pageable: true,
            height: 550,
            columns: [
                { field: "ModifiedBy", title: "نام کاربر" },
                { field: "ModifiedDate", title: "تاریخ تغییر" },
                { field: "LargeUnit", title: "تعداد واحد بزرگ", width: 150 },
                { field: "SmallUnit", title: "تعداد واحد کوچک", width: 150 },
            ],
            editable: false
        });
    };

    self.refreshStores();

    $(document).on("keypress", ".product-term", function (e) {
        var term = $(this).val();
        if (e.which == 13)
            self.refreshProducts(self.chosenStore(), term);
    });
};
//******************************************************************//

function UserManagerViewModel() {
    var self = this;

    self.chosenUser = ko.observable();
    self.users = ko.observableArray([]);
    self.stocks = ko.observableArray([]);
    self.chosenStocks = ko.observableArray([]);

    self.isInChosenStocks = function (data) {
        var match = ko.utils.arrayFirst(self.chosenStocks(), function (item) {
            return item.uniqueId === data.uniqueId;
        });

        if (match && match != null)
            return true;
        else
            return false;
    };

    self.addChosenStocks = function (data) {
        var match = ko.utils.arrayFirst(self.chosenStocks(), function (item) {
            return item.uniqueId === data.uniqueId;
        });

        if (match && match != null)
            self.chosenStocks.remove(data);
        else
            self.chosenStocks.push(data);
    };

    self.refreshUsers = function () {
        accountManagerApp.callApi(urls.usersUrl, 'GET', function (data) {
            self.users(data);
            if (self.users().length > 0)
                self.chosenUser(self.users()[0]);
        });
    };

    self.chosenUser.subscribe(function (newValue) {
        self.refreshStocks(newValue);
    }, self);

    self.refreshStocks = function (data) {
        self.chosenStocks([]);

        accountManagerApp.callApi(urls.storesUrl, 'GET', function (data) {
            self.stocks(data);

            accountManagerApp.callApi(urls.userStocksUrl, 'GET', function (data) {
                if (data)
                    data.forEach(function (itm) { self.addChosenStocks.push(itm); });
            });
        });
    };

    self.saveUsersStocks = function () {
        accountManagerApp.callApi(urls.saveStocksUsersUrl, 'POST', function (data) {
            showSuccess("ذخیره سازی", "انجام شد");
        });
    };

    self.refreshUsers();
};