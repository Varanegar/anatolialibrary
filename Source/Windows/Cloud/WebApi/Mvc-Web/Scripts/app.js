'use strict'
var baseBackendUrl = 'http://localhost:59822',
    sslBackendUrl = 'https://localhost:44300',
    privateOwnerId = '3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C',
    urls = {
        loginUrl: baseBackendUrl + '/oauth/token',
        storesUrl: baseBackendUrl + '/api/gateway/stock/stocks/', //?privateOwnerId=' + privateOwnerId,
        userStocksUrl: baseBackendUrl + '/api/gateway/stock/userStocks',
        saveStocksUsersUrl: baseBackendUrl + '/api/gateway/stock/saveUserStocks',
        searchProductsUrl: baseBackendUrl + '/api/gateway/product/searchProducts',
        stockProductsUrl: baseBackendUrl + '/api/gateway/stock/stockproduct/stockid/',
        saveStockProduct: baseBackendUrl + '/api/gateway/stock/stockproduct/save/?privateOwnerId=' + privateOwnerId,
        reorderCalcTypeUrl: baseBackendUrl + "/api/gateway/basedata/reordercalctypes",
        usersUrl: baseBackendUrl + "/api/accounts/users",
        permissionsUrl: baseBackendUrl + "/api/accounts/permissions",
        savePermissionsUrl: baseBackendUrl + "/api/accounts/savePermissions",
        permissionsOfUserUrl: baseBackendUrl + "/api/accounts/getPersmissionsOfUser",

        stocksUrl: baseBackendUrl + "/api/gateway/stock/stocks",
        stockTypesUrl: baseBackendUrl + '/api/gateway/basedata/stocktypes?privateOwnerId=' + privateOwnerId,
        saveStocksUrl: baseBackendUrl + '/api/gateway/stock/saveStocks',

        productsUrl: baseBackendUrl + '/api/gateway/product/products',
        saveProductsUrl: baseBackendUrl + '/api/gateway/product/saveProducts',
        productRequestRulesUrl: baseBackendUrl + '/api/gateway/stock/productRequestRules',
        productRequestRuleUrl: baseBackendUrl + '/api/gateway/stock/productRequestRule',
        productTypesUrl: baseBackendUrl + '/api/gateway/product/productTypes',

        StockProductRequestRuleTypesUrl: baseBackendUrl + '/api/gateway/stockproductrequest/stockProductRequestRuleTypes',
        StockProductRequestRuleCalcTypesUrl: baseBackendUrl + '/api/gateway/stockproductrequest/stockProductRequestRuleCalcTypes',

        SuppliersUrl: baseBackendUrl + '/api/gateway/base/supplier/suppliers?privateOwnerId=' + privateOwnerId,

        mainProductGroupsUrl: baseBackendUrl + '/api/gateway/product/mainProductGroupList',
        saveStockRequestProductUrl: baseBackendUrl + '/api/gateway/stockproductrequest/saveStockRequestProduct',

        stockProductRequestUrl: sslBackendUrl + '/api/gateway/stockproductrequest/requests',
        productRequestsHistory: sslBackendUrl + '/api/gateway/stockproductrequest/history',
        stockRequestReviewDetailsUrl: sslBackendUrl + '/api/gateway/stockproductrequest/detailshistory',
        stockProductRequestProductDetailsUrl: sslBackendUrl + '/api/gateway/stockproductrequest/requestProductDetails',
        updateStockRequestReviewDetailsUrl: sslBackendUrl + '/api/gateway/stockproductrequest/updateRequestProductDetails',

        myWebpages: baseBackendUrl + '/api/accounts/myWebpages',

        pages: {
            products: { url: '/products', title: 'محصولات' },
            stockproducts: { url: '/stocks/products', title: 'محصولات در انبار' },
            reviewproductrequest: { url: '/products/reviewProductRequest', title: 'بازنگری درخواست ها' },
            storerequestshistory: { url: '/products/storeRequestsHistory', title: 'سوابق درخواست ها' },
            stocks: { url: '/stocks', title: 'انبارها' },
            productrequestrules: { url: '/stocks/productRequestRules', title: 'قوانین کالاها' },
            usermanager: { url: '/userManager', title: 'تخصیص فروشگاه' },
            permissions: { url: '/userManager/permissions', title: 'تخصیص مجوز' },
        }
    },
errorMessage = {
    unAuthorized: "شما مجوز لازم را برای این درخواست ندارید!",
},
gridAuthHeader = function (req) {
    var tokenKey = 'accessToken',
        token = $.cookie("token");
    req.setRequestHeader('Authorization', 'Bearer ' + token);
    req.setRequestHeader('OwnerKey', privateOwnerId);
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
var showError = function (title, message) {
    toastr["error"](title, message);
};
var showSuccess = function (title, message) {
    toastr["success"](title, message);
};

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
};
//******************************************************************//

function headerMenuViewModel() {
    var self = this;
    self.shouldShowLogout = ko.observable(false);

    self.refreshHeaderMenu = function () {

        if ($(".header-menu .navbar-nav li").length < 2)
            accountManagerApp.callApi(urls.myWebpages, "POST", {}, function (data) {

                data.forEach(function (itm) {
                    if (itm.action !== '' && itm.action !== 'List') {

                        var url = urls.pages[itm.action.toLowerCase()].url;

                        var title = urls.pages[itm.action.toLowerCase()].title;

                        $(".header-menu .navbar-nav .exit-menu-item").before('<li><a href="' + url + '">' + title + '</a></li>');
                    }
                });
            });
    };

    self.shouldShowLogout.subscribe(function (newValue) {
        if (newValue === true) {

            // self.refreshHeaderMenu();
        }
    });

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

    self.callApi = function (url, callType, dataParams, callBackFunc) {
        if (url == undefined || url == '')
            return;

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
            headers: headers,
            data: dataParams
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
                self.callApi(retUrlObject.url, retUrlObject.callType, {}, retUrlObject.callBackFunc);

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

function stockProductManagerViewModel() {
    // Data
    var self = this;

    self.chosenStore = ko.observable();
    self.stores = ko.observableArray([]);
    self.products = ko.observableArray([]);
    self.flg_initGrid = ko.observable(true);

    self.refreshStores = function () {
        accountManagerApp.callApi(urls.storesUrl, 'POST', {}, function (data) {
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
        var dataSource = new kendo.data.DataSource({
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
        accountManagerApp.callApi(sslBackendUrl + '/api/TestAuth/getsample', 'POST', {}, function (data) {

        });
    }
    self.testAuth();
};
//******************************************************************//

function ReviewProductRequestViewModel() {
    var self = this;

    self.stockRequests = ko.observableArray([]);
    self.chosenStockProductRequest = ko.observable();
    self.storeRequestInfo = ko.observable();
    self.shouldShowRequestInfo = ko.observable(false);

    self.refreshStockRequest = function (term) {
        accountManagerApp.callApi(urls.stockProductRequestUrl, 'POST', { searchTerm: term }, function (data) {
            self.stockRequests(data);

            if (self.stockRequests().length > 0)
                self.refreshStoreRequestInfo(self.stockRequests()[0]);
        });
    };

    self.refreshStoreRequestInfo = function (data) {
        self.chosenStockProductRequest(data);
        self.initRequestReviewGrid();
        self.shouldShowRequestInfo(true);
    };

    self.initRequestReviewGrid = function () {
        debugger
        var dataSource = new kendo.data.DataSource({
            transport: {
                read: {
                    url: urls.stockRequestReviewDetailsUrl,
                    dataType: "json",
                    contentType: "application/json",
                    type: "POST",
                    data: { stockProductRequestId: self.chosenStockProductRequest().uniqueId },
                    beforeSend: gridAuthHeader
                },
                update: {
                    url: urls.updateStockRequestReviewDetailsUrl,
                    type: "POST",
                    contentType: "application/json",
                    dataType: "json",
                    data: { stockId: self.chosenStockProductRequest().stockId },
                    beforeSend: gridAuthHeader
                },
                parameterMap: function (options, operation) {

                    options.stockProductRequestId = self.chosenStockProductRequest().uniqueId;
                    options.stockId = self.chosenStockProductRequest().stockId;

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
                    id: "uniqueId",
                    fields: {
                        uniqueId: { editable: false, nullable: true },
                        productCode: { type: "string", editable: false },
                        productName: { type: "string", editable: false },
                        requestQty: { type: "number", editable: false },
                        accepted1Qty: { type: "number", editable: false },
                        accepted2Qty: { type: "number", editable: false },
                        accepted3Qty: { type: "number", editable: false },
                        myAcceptedQty: { type: "number", validation: { required: true, min: 1 } }
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
                // this.expandRow(this.tbody.find("tr.k-master-row").first());
            },
            toolbar: ["save"],
            columns: [
                { field: "productCode", title: "کد محصول" },
                { field: "productName", title: "نام محصول", width: 80 },
                { field: "requestQty", title: "تعداد سفارش", width: 80 },
                { field: "accepted1Qty", title: "نظر تاییدکننده اول", width: 80 },
                { field: "accepted2Qty", title: "نظر تاییدکننده دوم", width: 80 },
                { field: "accepted3Qty", title: "نظر تاییدکننده سوم", width: 80 },
                { field: "myAcceptedQty", title: "نظر من", width: 80 },
            ],
            editable: true
        });
    };

    function detailInit(e) {
        var detailRow = e.detailRow;
        debugger
        detailRow.find(".request-details-grid").kendoGrid({
            dataSource: {
                transport: {
                    read: {
                        url: urls.stockProductRequestProductDetailsUrl,
                        dataType: "json",
                        contentType: "application/json",
                        type: "POST",
                        data: { stockProductRequestProductId: e.data.Id },
                        beforeSend: gridAuthHeader
                    },
                    parameterMap: function (options, operation) {
                        options.stockProductRequestProductId = e.data.Id;
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
                { field: "stockProductRequestRuleName", title: "شماره قانون", width: "150px" },
                { field: "requestQty", title: "تعداد", width: "150px" },
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

        window.location = urls.pages.stockproducts.url + "?stockId=" + id;
    });
};
//******************************************************************//

function ProductHistoryManagerViewModel() {
    var self = this;

    self.chosenStore = ko.observable();
    self.stores = ko.observableArray([]);

    self.chosenStore.subscribe(function (newValue) {
        self.refreshHistory(true);
    }, self);

    self.refreshStores = function () {
        accountManagerApp.callApi(urls.stocksUrl, 'POST', {}, function (data) {
            self.stores(data);
        });
    };

    self.refreshHistory = function (initGrid_flag) {
        if (initGrid_flag)
            self.initGrid(self.chosenStore().uniqueId);
        else {
            $('.history-grid').data("kendoGrid").dataSource.read();
            $('.history-grid').data("kendoGrid").refresh();
        }
    };

    self.initGrid = function (id) {
        var dataSource = new kendo.data.DataSource({
            transport: {
                read: {
                    url: urls.productRequestsHistory,
                    dataType: "json",
                    contentType: "application/json",
                    type: "POST",
                    data: { stockId: self.chosenStore().uniqueId },
                    beforeSend: gridAuthHeader
                },
                parameterMap: function (options, operation) {
                    options.stockId = self.chosenStore().uniqueId;
                    if (operation == "read")
                        return kendo.stringify(options);
                }
            },
            batch: true,
            pageSize: 20,
            schema: {
                model: {
                    id: "uniqueId",
                    fields: {
                        uniqueId: { editable: false, nullable: true },
                        sourceStockRequestNo: { type: "string" },
                        requestPDate: { type: "string" },
                        acceptName1: { type: "string" },
                        acceptName2: { type: "string" },
                        acceptName3: { type: "string" },
                        stockProductRequestStatus: { type: "string" },
                        sendToSourceStockDatePDate: { type: "string" },
                        targetStockIssueDatePDate: { type: "string" },
                    }
                }
            }
        });

        $(".history-grid").kendoGrid({
            dataSource: dataSource,
            navigatable: true,
            pageable: true,
            height: 550,
            detailTemplate: kendo.template($("#template").html()),
            detailInit: detailInit,
            columns: [
                { field: "sourceStockRequestNo", title: "شماره درخواست" },
                { field: "requestPDate", title: "تاریخ درخواست" },
                { field: "acceptName1", title: "تایید کننده اول" },
                { field: "acceptName2", title: "تایید کننده دوم" },
                { field: "acceptName3", title: "تایید کننده سوم" },
                { field: "stockProductRequestStatus", title: "وضعیت" },
                { field: "sendToSourceStockDatePDate", title: "تاریخ ارسال" },
                { field: "targetStockIssueDatePDate", title: "تاریخ تحویل" },
            ],
            editable: false
        });
    };

    function detailInit(e) {
        var detailRow = e.detailRow;

        detailRow.find(".request-details-grid").kendoGrid({
            dataSource: {
                transport: {
                    read: {
                        url: urls.stockRequestReviewDetailsUrl,
                        dataType: "json",
                        contentType: "application/json",
                        type: "POST",
                        data: { stockProductRequestId: e.data.Id }
                    },
                    parameterMap: function (options, operation) {
                        options.stockProductRequestId = e.data.Id;
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
                { field: "productCode", title: "کد کالا", width: "70px" },
                { field: "productName", title: "نام کالا", width: "150px" },
                { field: "accepted1Qty", title: "تعداد تایید شده اول", width: "150px" },
                { field: "accepted2Qty", title: "تعداد تایید شده دوم", width: "150px" },
                { field: "accepted3Qty", title: "تعداد تایید شده سوم", width: "150px" },
                { field: "requestQty", title: "تعداد ارسال شده", width: "150px" },
                { field: "deliveredQty", title: "تعداد تحویل شده", width: "150px" },
            ]
        });
    }

    self.refreshStores();
};
//******************************************************************//

function UsersStocksViewModel() {
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
            self.chosenStocks.remove(match);
        else
            self.chosenStocks.push(data);
    };

    self.refreshUsers = function () {
        accountManagerApp.callApi(urls.usersUrl, 'GET', {}, function (data) {
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
        var userId = data.id;

        accountManagerApp.callApi(urls.storesUrl, 'POST', {}, function (data) {
            self.stocks(data);

            accountManagerApp.callApi(urls.userStocksUrl, 'POST', { userId: userId }, function (data) {
                if (data && data.length > 0)
                    data.forEach(function (itm) { self.addChosenStocks(itm); });
            });
        });
    };

    self.saveUsersStocks = function () {
        var stockIds = [];

        self.chosenStocks().forEach(function (stock) { stockIds.push(stock.uniqueId); });

        accountManagerApp.callApi(urls.saveStocksUsersUrl, 'POST', { userId: self.chosenUser().id, stockIds: stockIds }, function (data) {
            showSuccess("ذخیره سازی", "انجام شد");
        });
    };

    self.refreshUsers();
};
//******************************************************************//

function UserPermissionsViewModel() {
    var self = this;

    self.chosenUser = ko.observable();
    self.users = ko.observableArray([]);
    self.permissions = ko.observableArray([]);

    self.refreshUsers = function () {
        accountManagerApp.callApi(urls.usersUrl, 'GET', {}, function (data) {
            self.users(data);
            if (self.users().length > 0)
                self.chosenUser(self.users()[0]);
        });
        self.refreshPermissions();
    };

    self.chosenUser.subscribe(function (newValue) {
        $('input[name="grants[]"]:checked').each(function (indx, elm) {
            $(this).prop('checked', false);
        });

        accountManagerApp.callApi(urls.permissionsOfUserUrl, 'POST', { userId: self.chosenUser().id }, function (data) {
            data.forEach(function (itm) {
                if (itm.grant)
                    $('input[name="grants[]"][data-id="' + itm.permissionId + '"]').prop("checked", true);
            });
        });
    }, self);

    self.refreshPermissions = function () {
        accountManagerApp.callApi(urls.permissionsUrl, 'POST', {}, function (data) {
            self.permissions(data);
            self.initTreeView(data);
        });
    };

    self.initTreeView = function (data) {
        data.forEach(function (itm) {
            itm["expanded"] = true;
        });

        $(".permissions-tree-view").kendoTreeView({
            dataTextField: "title",
            loadOnDemand: false,
            checkboxes: {
                template: "<input type='checkbox' name='grants[]' data-id='#= item.id #' />"
            },
            dataSource: {
                transport: {
                    read: function (options) {

                        var id = options.data.id || "";

                        options.success($.grep(data, function (x) {
                            return x.parent == id;
                        }));
                    }
                },
                schema: {
                    model: {
                        id: "id",
                        hasChildren: function (x) {
                            var id = x.id;

                            for (var i = 0; i < data.length; i++) {
                                if (data[i].parent == id) {
                                    return true;
                                }
                            }
                            return false;
                        }
                    }
                }
            },
        });
    };

    self.saveUserPermissions = function () {
        var _permissions = [];
        $('input[name="grants[]"]:checked').each(function (indx, elm) {
            var p = { id: $(this).attr('data-id'), grant: true };
            _permissions.push(p);
        });

        accountManagerApp.callApi(urls.savePermissionsUrl, 'POST', { data: JSON.stringify({ userId: self.chosenUser().id, permissions: _permissions }) }, function (data) {
            showSuccess('', 'اطلاعات ذخیره گردید');
        });
    };

    self.refreshUsers();
};
//******************************************************************//

function stocksManagerViewModel() {
    // Data
    var self = this;

    self.initStocksGrid = function (id) {
        var dataSource = new kendo.data.DataSource({
            transport: {
                read: {
                    url: urls.stocksUrl,
                    dataType: "json",
                    contentType: "application/json",
                    type: "POST",
                    data: {},
                    beforeSend: gridAuthHeader
                },
                update: {
                    url: urls.saveStocksUrl,
                    type: "POST",
                    contentType: "application/json",
                    dataType: "json",
                    beforeSend: gridAuthHeader
                },
                parameterMap: function (options, operation) {
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
                    id: "uniqueId",
                    fields: {
                        uniqueId: { editable: false, nullable: true },
                        stockCode: { editable: false },
                        stockName: { editable: false },
                        approver1: { defaultValue: { uniqueId: "", userName: "" } },
                        approver2: { defaultValue: { uniqueId: "", userName: "" } },
                        approver3: { defaultValue: { uniqueId: "", userName: "" } },
                        stockType: { defaultValue: { uniqueId: "", stockTypeName: "" } },
                        mainStock: { defaultValue: { uniqueId: "", stockName: "" } },
                        relatedStock: { defaultValue: { uniqueId: "", stockName: "" } }
                    }
                }
            }
        });

        $(".stocks-grid").kendoGrid({
            dataSource: dataSource,
            navigatable: true,
            pageable: true,
            height: 650,
            toolbar: ["save", "cancel"],
            columns: [
                { field: "stockCode", title: "کد انبار", width: 100 },
                { field: "stockName", title: "نام انبار", width: 150 },
                { field: "approver1", title: "تایید کننده اول", width: "150px", editor: approverDropDownEditor, template: "#=approver1.userName#" },
                { field: "approver2", title: "تایید کننده دوم", width: "150px", editor: approverDropDownEditor, template: "#=approver2.userName#" },
                { field: "approver3", title: "تایید کننده سوم", width: "150px", editor: approverDropDownEditor, template: "#=approver3.userName#" },
                { field: "stockType", title: "نوع انبار", width: "150px", editor: stockTypeDropDownEditor, template: "#=stockType.stockTypeName#" },
                { field: "mainStock", title: "انباراصلی", width: "150px", editor: nestedStockDropDownEditor, template: "#=mainStock.stockName#" },
                { field: "relatedStock", title: "تامین کننده", width: "150px", editor: nestedStockDropDownEditor, template: "#=relatedStock.stockName#" },
            ],
            editable: true
        });

        var dropdownEditorParameterMap = function (options, operation) {
            if (operation == "read")
                return kendo.stringify(options);
        };

        function approverDropDownEditor(container, options) {
            $('<input required data-text-field="userName" data-value-field="uniqueId" data-bind="value:' + options.field + '"/>')
                .appendTo(container)
                .kendoDropDownList({
                    dataSource: {
                        transport: {
                            read: {
                                url: urls.usersUrl,
                                dataType: "json",
                                contentType: "application/json",
                                type: "GET",
                                beforeSend: gridAuthHeader,
                            },
                            parameterMap: dropdownEditorParameterMap
                        }
                    },
                    change: function (e) {
                        var dataItem = e.sender.dataItem();

                        dataItem["uniqueId"] = dataItem.id;
                        options.model.set(options.field, dataItem);
                    }
                });
        }
        function stockTypeDropDownEditor(container, options) {
            $('<input required data-text-field="stockTypeName" data-value-field="uniqueId" data-bind="value:' + options.field + '"/>')
                .appendTo(container)
                .kendoDropDownList({
                    dataSource: {
                        transport: {
                            read: {
                                url: urls.stockTypesUrl,
                                dataType: "json",
                                contentType: "application/json",
                                type: "GET",
                                beforeSend: gridAuthHeader,
                            },
                            parameterMap: dropdownEditorParameterMap
                        }
                    },
                    change: function (e) {
                        var dataItem = e.sender.dataItem();

                        options.model.set(options.field, dataItem);
                    }
                });
        }
        function nestedStockDropDownEditor(container, options) {
            $('<input required data-text-field="stockName" data-value-field="uniqueId" data-bind="value:' + options.field + '"/>')
                .appendTo(container)
                .kendoDropDownList({
                    dataSource: {
                        transport: {
                            read: {
                                url: urls.stocksUrl,
                                dataType: "json",
                                contentType: "application/json",
                                type: "POST",
                                beforeSend: gridAuthHeader,
                            },
                            parameterMap: dropdownEditorParameterMap
                        }
                    },
                    change: function (e) {
                        var dataItem = e.sender.dataItem();

                        options.model.set(options.field, dataItem);
                    }
                });
        }

    };

    self.initStocksGrid();
};
//******************************************************************//

function productRequestRulesManagerViewModel() {
    // Data
    var self = this;

    self.initProductRulesGrid = function (id) {
        var dataSource = new kendo.data.DataSource({
            transport: {
                read: {
                    url: urls.productRequestRulesUrl,
                    dataType: "json",
                    contentType: "application/json",
                    type: "POST",
                    data: {},
                    beforeSend: gridAuthHeader
                },
                parameterMap: function (options, operation) {
                    if (operation == "read")
                        return kendo.stringify(options);
                }
            },
            batch: true,
            pageSize: 20,
            schema: {
                model: {
                    id: "uniqueId",
                    fields: {
                        uniqueId: { editable: false, nullable: true },
                        stockProductRequestRuleName: { editable: false },
                        fromPDate: { editable: false },
                        topDate: { editable: false },
                    }
                }
            }
        });

        $(".product-rules-grid").kendoGrid({
            dataSource: dataSource,
            navigatable: true,
            pageable: true,
            toolbar: kendo.template($("#toolbar-template").html()),
            height: 500,
            columns: [
                { field: "stockProductRequestRuleName", title: "نام", width: 100 },
                { field: "fromPDate", title: "از تاریخ", width: 150 },
                { field: "topDate", title: "تا تاریخ", width: 150 },
                { command: { text: "ویرایش", click: self.showEdit }, title: " ", width: "180px" }
            ],
            editable: false
        });
    };

    self.addProductRequestRule = function () {
        self.openWindow("/stocks/productRequestRule");
    }

    self.openWindow = function (url) {
        $('.product-rules-page').append('<div class="edit-window"></div>');
        var editWindow = $(".edit-window").kendoWindow({
            title: "ویرایش",
            content: url,
            deactivate: function () {
                this.destroy();
            },
            modal: true,
            visible: false,
            resizable: true,
            width: 800,
            actions: [
                "Pin",
                "Minimize",
                "Maximize",
                "Close"
            ],
            close: onClose
        }).data("kendoWindow").center().maximize().open();;
    }

    function onClose() {
        $('.product-rules-grid').data('kendoGrid').dataSource.read();
        $('.product-rules-grid').data('kendoGrid').refresh();
    }

    self.showEdit = function (e) {
        e.preventDefault();

        var dataItem = this.dataItem($(e.currentTarget).closest("tr"));

        self.openWindow("/stocks/productRequestRule?id=" + dataItem.uniqueId);
    }

    self.initProductRulesGrid();
};

function productRequestRuleEditManagerViewModel() {
    // Data
    var self = this;

    function dropdownParameterMap(options, operation) {
        if (operation == "read")
            return kendo.stringify(options);
    }

    self.refreshReorderCalcType = function () {
        $("#reOrderCalcType").kendoDropDownList({
            dataTextField: "reorderTypeName",
            dataValueField: "uniqueId",
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
                    parameterMap: dropdownParameterMap
                }
            },
            dataBound: function () {

                if (self.selectedRule() && self.selectedRule() !== '')
                    this.value(self.selectedRule().reorderCalcTypeId);
            }
        });
    }

    self.refreshProductType = function () {
        $("#productType").kendoDropDownList({
            dataTextField: "productTypeName",
            dataValueField: "uniqueId",
            dataSource: {
                transport: {
                    read: {
                        url: urls.productTypesUrl,
                        dataType: "json",
                        contentType: "application/json",
                        type: "POST",
                        beforeSend: gridAuthHeader
                    },
                    parameterMap: dropdownParameterMap
                },
            },
            dataBound: function () {
                if (self.selectedRule() && self.selectedRule() !== '')
                    this.value(self.selectedRule().productTypeId);
            }
        });
    }

    self.refreshStockProductRequestRuleTypes = function () {
        $("#stockProductRequestRuleType").kendoDropDownList({
            dataTextField: "stockProductRequestRuleTypeName",
            dataValueField: "uniqueId",
            dataSource: {
                transport: {
                    read: {
                        url: urls.StockProductRequestRuleTypesUrl,
                        dataType: "json",
                        contentType: "application/json",
                        type: "POST",
                        beforeSend: gridAuthHeader
                    },
                    parameterMap: dropdownParameterMap
                }
            },
            dataBound: function () {
                if (self.selectedRule() && self.selectedRule() !== '')
                    this.value(self.selectedRule().ruleTypeId);
            }
        });
    }

    self.refreshStockProductRequestRuleCalcTypes = function () {
        $("#stockProductRequestRuleCalcType").kendoDropDownList({
            dataTextField: "stockProductRequestRuleCalcTypeName",
            dataValueField: "uniqueId",
            dataSource: {
                transport: {
                    read: {
                        url: urls.StockProductRequestRuleCalcTypesUrl,
                        dataType: "json",
                        contentType: "application/json",
                        type: "POST",
                        beforeSend: gridAuthHeader
                    },
                    parameterMap: dropdownParameterMap
                }
            },
            dataBound: function () {
                if (self.selectedRule() && self.selectedRule() !== '')
                    this.value(self.selectedRule().ruleCalcTypeId);
            }
        });
    }

    self.refreshSuppliers = function () {
        $("#suppliers").kendoDropDownList({
            dataTextField: "supplierName",
            dataValueField: "uniqueId",
            dataSource: {
                transport: {
                    read: {
                        url: urls.SuppliersUrl,
                        dataType: "json",
                        contentType: "application/json",
                        type: "Get",
                        beforeSend: gridAuthHeader
                    },
                    parameterMap: dropdownParameterMap
                }
            }
        });
    }

    self.selectedProductId = ko.observable('');
    self.refreshProduct = function () {
        $("#product").kendoAutoComplete({
            dataTextField: "name",
            minLength: 3,
            delay: 500,
            dataSource: {
                transport: {
                    read: {
                        url: urls.searchProductsUrl,
                        dataType: "json",
                        contentType: "application/json",
                        type: "POST",
                        beforeSend: gridAuthHeader,
                    },
                    parameterMap: function (options, operation) {
                        options.searchTerm = $("#product").val();

                        if (operation == "read")
                            return kendo.stringify(options);
                    }
                }
            },
            select: function (e) {
                var dataItem = this.dataItem(e.item.index());

                self.selectedProductId(dataItem.id);
            }
        });
    }

    self.mainProductGroups = ko.observableArray([]);
    self.selectedMainProductGroupId = ko.observable('');
    self.initMainProductGroup = function (data) {
        data.forEach(function (itm) {
            itm["expanded"] = true;
        });

        $(".main-product-group-tree-view").kendoTreeView({
            dataTextField: "groupName",
            loadOnDemand: false,
            dataSource: {
                transport: {
                    read: function (options) {
                        var id = options.data.id || "00000000-0000-0000-0000-000000000000";

                        options.success($.grep(data, function (x) {
                            return x.parent == id;
                        }));
                    }
                },
                schema: {
                    model: {
                        id: "id",
                        hasChildren: function (x) {
                            var id = x.id;

                            for (var i = 0; i < data.length; i++) {
                                if (data[i].parent == id) {
                                    return true;
                                }
                            }

                            return false;
                        }
                    }
                }
            },
            select: function (e) {
                var dataItem = this.dataItem(e.node);

                self.selectedMainProductGroupId(dataItem.id);
            }
        });
    }
    self.refreshMainProductGroup = function () {
        accountManagerApp.callApi(urls.mainProductGroupsUrl, 'POST', {}, function (data) {
            self.mainProductGroups(data);
            self.initMainProductGroup(data);
        });
    }

    self.selectedRule = ko.observable('');
    self.checkEditMode = function () {
        var id = $('#ruleId').val();
        if (id)
            accountManagerApp.callApi(urls.productRequestRuleUrl, "POST", { ruleId: id }, function (data) {
                $('#ruleId').val(data.uniqueId);
                $('#ruleName').val(data.stockProductRequestRuleName);
                $('#fromDate').val(data.fromPDate);
                $('#toDate').val(data.topDate);
                $('#quantity').val(data.qty);

                self.selectedRule(data);

                $("#productType").data("kendoDropDownList").value(data.productTypeId);
                $('#reOrderCalcType').data("kendoDropDownList").value(data.reorderCalcTypeId);

                $('#stockProductRequestRuleCalcType').data("kendoDropDownList").value(data.ruleCalcTypeId);
                $('#stockProductRequestRuleType').data("kendoDropDownList").value(data.ruleTypeId);

                if (data.supplierId && data.supplierId !== '') {
                    $('#suppliers').data("kendoDropDownList").value(data.supplierId);
                    if ($('#via-filter').val() !== 1)
                        $('#via-filter').val(1).trigger("change");
                }

                if (data.productId && data.productId !== '') {

                    self.selectedProductId(data.productId);
                    $('#product').val(data.productName);

                    if ($('#via-filter').val() !== 2)
                        $('#via-filter').val(2).trigger("change");
                }

                if (data.mainProductGroupId && data.mainProductGroupId !== '') {

                    var treeview = $(".main-product-group-tree-view").data("kendoTreeView");
                    var getitem = treeview.dataSource.get(data.mainProductGroupId);
                    treeview.findByUid(getitem.uid);
                    var selectitem = treeview.findByUid(getitem.uid);
                    treeview.select(selectitem);

                    if ($('#via-filter').val() !== 3)
                        $('#via-filter').val(3).trigger("change");
                }

            });

    };

    self.refreshReorderCalcType();
    self.refreshProductType();
    self.refreshStockProductRequestRuleTypes();
    self.refreshStockProductRequestRuleCalcTypes();
    self.refreshSuppliers();
    self.refreshProduct();
    self.refreshMainProductGroup();
    self.checkEditMode();

    $(".product-rule-edit-page").on("change", "#via-filter", function () {
        var id = $(this).val();

        $('.row[data-via-filter]').addClass('hide');
        $('.row[data-via-filter=' + id + ']').removeClass('hide');
    });

    $(".product-rule-edit-page").on("click", '.btn-cancel', function (e) {
        e.preventDefault;
        $(".edit-window").data("kendoWindow").close();
    });

    $(".product-rule-edit-page").on("click", '.btn-save', function (e) {
        e.preventDefault;

        var data = {
            ruleId: $('#ruleId').val(),
            ruleName: $('input[name=ruleName]').val(),
            fromDate: $('input[name=fromDate]').val(),
            toDate: $('input[name=toDate]').val(),
            quantity: $('input[name=quantity]').val(),
            productType: $('#productType').val(),
            reOrderCalcType: $('#reOrderCalcType').val(),
            stockProductRequestRuleType: $('#stockProductRequestRuleType').val(),
            stockProductRequestRuleCalcType: $('#stockProductRequestRuleCalcType').val(),
        };

        var viaFilter = $('#via-filter').val();
        if (viaFilter === "1")
            data["supplierId"] = $('#suppliers').val();
        if (viaFilter === "2")
            data["productId"] = self.selectedProductId();
        if (viaFilter === "3")
            data["mainProductGroupId"] = self.selectedMainProductGroupId();

        var errors = [];
        if (data.ruleName === '')
            errors.push('نام را وارد نمایید');
        if (data.fromDate === '' || data.toDate === '')
            errors.push('تاریخ را انتخاب نمایید');
        if (viaFilter === "2" && data["productId"] === '')
            errors.push('نام محصول را وارد نمایید');
        if (viaFilter === "3" && data["mainProductGroupId"] === '')
            errors.push('گروه اصلی محصول را انتخاب نمایید');

        if (errors.length > 0) {
            errors.forEach(function (err) {
                showError('', err);
            });
            return;
        }
        console.log(JSON.stringify(data));

        accountManagerApp.callApi(urls.saveStockRequestProductUrl, 'POST', { stockRequestProduct: JSON.stringify(data) }, function (data) {
            $(".edit-window").data("kendoWindow").close();
        });
    });

};
//******************************************************************//

function productManagerViewModel() {
    // Data
    var self = this;

    self.initGrid = function () {
        var dataSource = new kendo.data.DataSource({
            transport: {
                read: {
                    url: urls.productsUrl,
                    dataType: "json",
                    contentType: "application/json",
                    type: "POST",
                    beforeSend: gridAuthHeader
                },
                update: {
                    url: urls.saveProductsUrl,
                    type: "POST",
                    contentType: "application/json",
                    dataType: "json",
                    beforeSend: gridAuthHeader
                },
                parameterMap: function (options, operation) {
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
                    id: "uniqueId",
                    fields: {
                        uniqueId: { editable: false, nullable: true },
                        productCode: { editable: false },
                        productName: { editable: false },
                        productTypeInfo: { defaultValue: { uniqueId: "", productTypeName: "" } },
                        mainSupplierName: { editable: false },
                        manufactureName: { editable: false }
                    }
                }
            }
        });

        $(".products-grid").kendoGrid({
            dataSource: dataSource,
            navigatable: true,
            pageable: true,
            height: 550,
            toolbar: ["save", "cancel"],
            columns: [
                { field: "productCode", title: "کد کالا", width: 100 },
                { field: "productName", title: "نام کالا", width: 200 },
                { field: "productTypeInfo", title: "نوع کالا", width: "180px", editor: categoryDropDownEditor, template: "#=productTypeInfo.productTypeName#" },
                { field: "mainSupplierName", title: "تامین کننده", width: 200 },
                { field: "manufactureName", title: "تولید کننده", width: 200 },
            ],
            editable: true
        });

        function categoryDropDownEditor(container, options) {

            $('<input required data-text-field="productTypeName" data-value-field="uniqueId" data-bind="value:' + options.field + '"/>')
                .appendTo(container)
                .kendoDropDownList({
                    dataSource: {
                        transport: {
                            read: {
                                url: urls.productTypesUrl,
                                dataType: "json",
                                contentType: "application/json",
                                type: "POST",
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

    self.initGrid();
};
//******************************************************************//