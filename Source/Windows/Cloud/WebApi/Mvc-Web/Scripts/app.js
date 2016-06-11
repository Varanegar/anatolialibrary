//'use strict'
//var baseBackendUrl = 'http://46.209.104.2:7000',
//    sslBackendUrl = 'https://localhost:443',
//var baseBackendUrl = 'http://217.218.53.71:8090/',
//var baseBackendUrl = 'http://192.168.0.160:7070/',
var baseBackendUrl = 'http://localhost:59822';
//sslBackendUrl = 'http://localhost',

privateOwnerId = '79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240',
dataOwnerId = '3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C',
dataOwnerCenterId = '3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C',
urls = {
    //loginUrl: baseBackendUrl + '/oauth/token',
    loginUrl: baseBackendUrl + '/api/identityAccounts/login',
    storesUrl: baseBackendUrl + '/api/gateway/stock/stocks/',
    userStocksUrl: baseBackendUrl + '/api/gateway/stock/userStocks',
    saveStocksUsersUrl: baseBackendUrl + '/api/gateway/stock/saveUserStocks',
    searchProductsUrl: baseBackendUrl + '/api/gateway/product/searchProducts',
    stockProductsUrl: baseBackendUrl + '/api/gateway/stock/stockproduct/stockid/',
    saveStockProduct: baseBackendUrl + '/api/gateway/stock/stockproduct/save/',
    reorderCalcTypeUrl: baseBackendUrl + "/api/gateway/basedata/reordercalctypes",

    usersUrl: baseBackendUrl + "/api/accounts/users",
    userUrl: baseBackendUrl + "/api/accounts/getUser",
    saveUserUrl: baseBackendUrl + '/api/accounts/saveUser',
    checkUserEmail: baseBackendUrl + '/api/accounts/checkEmailExist',

    permissionCatalogsUrl: baseBackendUrl + "/api/accounts/permissionCatalogs",
    permissionCatalogsOfUserUrl: baseBackendUrl + "/api/accounts/getPersmissionCatalogsOfUser",
    savePermissionCatalogsUrl: baseBackendUrl + "/api/accounts/savePermissionCatalogs",

    permissionsUrl: baseBackendUrl + "/api/accounts/permissions",
    savePermissionsUrl: baseBackendUrl + "/api/accounts/savePermissions",
    permissionsOfUserUrl: baseBackendUrl + "/api/accounts/getPersmissionsOfUser",

    stocksUrl: baseBackendUrl + "/api/gateway/stock/stocks",
    stockTypesUrl: baseBackendUrl + '/api/gateway/basedata/stocktypes',
    saveStocksUrl: baseBackendUrl + '/api/gateway/stock/saveStocks',

    productsUrl: baseBackendUrl + '/api/gateway/product/products/compress',
    saveProductsUrl: baseBackendUrl + '/api/gateway/product/saveProducts',

    productRequestRulesUrl: baseBackendUrl + '/api/gateway/stock/productRequestRules',
    productRequestRuleUrl: baseBackendUrl + '/api/gateway/stock/productRequestRule',
    productTypesUrl: baseBackendUrl + '/api/gateway/product/productTypes',

    StockProductRequestRuleTypesUrl: baseBackendUrl + '/api/gateway/stockproductrequest/stockProductRequestRuleTypes',
    StockProductRequestRuleCalcTypesUrl: baseBackendUrl + '/api/gateway/stockproductrequest/stockProductRequestRuleCalcTypes',

    SuppliersUrl: baseBackendUrl + '/api/gateway/base/supplier/suppliers',
    filterSuppliersUrl: baseBackendUrl + '/api/gateway/base/supplier/filterSuppliers',

    mainProductGroupsUrl: baseBackendUrl + '/api/gateway/product/mainProductGroupList',
    filterMainProductGroupsUrl: baseBackendUrl + '/api/gateway/product/filterMainProductGroupList',
    saveStockRequestProductUrl: baseBackendUrl + '/api/gateway/stockproductrequest/saveStockRequestProduct',

    stockProductRequestUrl: baseBackendUrl + '/api/gateway/stockproductrequest/requests',
    productRequestsHistory: baseBackendUrl + '/api/gateway/stockproductrequest/history',
    stockRequestReviewDetailsUrl: baseBackendUrl + '/api/gateway/stockproductrequest/detailshistory',
    stockProductRequestProductDetailsUrl: baseBackendUrl + '/api/gateway/stockproductrequest/requestProductDetails',
    updateStockRequestReviewDetailsUrl: baseBackendUrl + '/api/gateway/stockproductrequest/updateRequestProductDetails',

    myWebpages: baseBackendUrl + '/api/accounts/myWebpages',

    sendPassCodeUrl: baseBackendUrl + '/api/accounts/SendPassCode',
    resetPasswordByCodeUrl: baseBackendUrl + '/api/accounts/ResetPasswordByCode',
    changePasswordUrl: baseBackendUrl + '/api/accounts/ChangePassword',

    pages: {
        product: { url: '/Products', title: 'کالا', order: 7 },
        stockproduct: { url: '/Stocks/products', title: 'کالای انبار', order: 9 },
        stockproductrequest: { url: '/Products/reviewProductRequest', title: 'بازنگری درخواست ها', order: 4 },
        stockproductrequesthistory: { url: '/Products/storeRequestsHistory', title: 'سوابق درخواست ها', order: 5 },
        stock: { url: '/Stocks', title: 'انبارها', order: 8 },
        productrequestrules: { url: '/Stocks/productRequestRules', title: 'قوانین', order: 6 },
        usermanagement: { url: '/UserManager', title: 'مدیریت کاربران', order: 1 },
        userstock: { url: '/UserManager/stocks', title: 'تخصیص انبار', order: 3 },
        permission: { url: '/UserManager/permissions', title: 'مجوز دسترسی', order: 2 },
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
    unfreezUI();
};
var showSuccess = function (title, message) {
    toastr["success"](title, message);
};

var onRequestEnd = function (e) {
    if (e.type == "update" && !e.response.Errors)
        showSuccess('', 'اطلاعات ثبت شد.');

    if (e.type == "create" && !e.response.Errors)
        showSuccess('', 'اطلاعات ثبت شد.');
}

var rowNumber = 0;
function resetRowNumber(e) {
    rowNumber = 0;
}
function renderNumber(data) {
    return ++rowNumber;
}
var onDataBinding = function () {
    rowNumber = (this.dataSource.page() - 1) * this.dataSource.pageSize();
}
//******************************************************************//
var freezUI = function () {
    $.blockUI.defaults.css.border = '2px solid black';
    $.blockUI.defaults.css.padding = '5px 5px';
    $.blockUI.defaults.css.height = '40px';
    $.blockUI({ message: '<img src="/Content/KendoUI/images/loading-blue-bak.gif" height="28" width="28" />     لطفا کمی صبر نمایید...' });
    //$('#myDiv').block({ message: 'Processing...' });
};

var unfreezUI = function () {
    $.unblockUI({ fadeOut: 200 });
};
//******************************************************************//

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

        if ($(".header-menu .navbar-nav li").length < 2) self.shouldShowLogout(true);

        //$(".header-menu .navbar-nav").html("<li class='exit-menu-item'' data-bind='visible: shouldShowLogout'><a href='#'' class='glyphicon glyphicon-log-out'> خروج </a></li>");

        var $wrapper = $('.header-menu ul.navbar-nav');

        $wrapper.find('li').sort(function (a, b) {
            return +a.dataset.order - +b.dataset.order;
        }).appendTo($wrapper);
    };

    self.shouldShowLogout.subscribe(function (newValue) {
        if (newValue === true) {
            accountManagerApp.callApi(urls.myWebpages, "POST", {}, function (data) {
                if (data != "") {
                    data.forEach(function (itm) {
                        if (itm.action !== '' && itm.action !== 'List') {

                            var url = urls.pages[itm.resource.toLowerCase()].url;

                            var title = urls.pages[itm.resource.toLowerCase()].title;

                            var order = urls.pages[itm.resource.toLowerCase()].order;

                            $(".header-menu .navbar-nav .exit-menu-item").before('<li data-order=' + order + '><a href="' + url + '">' + title + '</a></li>');
                        }
                    });
                }

            });
            self.refreshHeaderMenu();
        }
        else {
            $(".header-menu").html("<div class='container'><div class='navbar-header'><button type='button' class='navbar-toggle' data-toggle='collapse' data-target='.navbar-collapse'><span class='icon-bar'></span><span class='icon-bar'></span><span class='icon-bar'></span></button><a class='navbar-brand' href='/Products/ReviewProductRequest'>مدیریت تامین کالا</a></div><div class='navbar-collapse collapse'><ul class='nav navbar-nav'><li class='exit-menu-item' '='' data-bind='visible: shouldShowLogout'><a href='#' '='' class='glyphicon glyphicon-log-out'> خروج </a></li></ul></div></div>");
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
    self.resetCode = ko.observable('');
    self.newPass = ko.observable('');
    self.confirmNewPass = ko.observable('');

    function showAjaxError(jqXHR) {

        var title = jqXHR.status,
            message = jqXHR.statusText

        if (jqXHR.responseJSON) {
            title = jqXHR.responseJSON.error;
            message = jqXHR.responseJSON.error_description;
        }

        if (jqXHR.status == 400)
        {
            title = '400';
            message = jqXHR.responseJSON.message;
        }

        if (jqXHR.status == 401) {
            title = '401';
            message = errorMessage.unAuthorized;
        }
        if (jqXHR.status == 403) {
            title = '403';
            message = errorMessage.unAuthorized;
        }
        if (jqXHR.status == 400 && jqXHR.responseJSON.modelState != undefined) {
            title = 'خطا';
            var modelState = jqXHR.responseJSON.modelState;
            var errorsString = "";
            var errors = [];
            for (var key in modelState) {
                if (modelState.hasOwnProperty(key)) {
                    errorsString = (errorsString == "" ? "" : errorsString + "<br/>") + modelState[key];
                    errors.push(modelState[key]);//list of error messages in an array
                }
            }
            message = errorsString;
        }
        if (jqXHR.status == 400 && jqXHR.responseJSON.modelState == undefined) {
            title = 'خطا';
            message = jqXHR.responseJSON.message;
        }

        showError(title, message);

        console.log(title + ': ' + message);
    }

    self.callApi = function (url, callType, dataParams, callBackFunc) {
        freezUI();
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

            unfreezUI();

        }).fail(function (jqXHR) {
            showAjaxError(jqXHR);

            if (jqXHR.status == 401) {
                self.requestAppObject({ url: url, callType: callType, callBackFunc: callBackFunc });
                self.openLogin();
            }

            unfreezUI();
        });
    }

    var $loginForm = $(".login-form");
    self.login = function () {
        self.result('');

        if (self.loginEmail() == undefined || self.loginEmail() == '' || self.loginPassword() == undefined || self.loginPassword() == '') {
            showError('', 'لطفا اطلاعات کاربری خود را وارد نمایید');
            $(".email-txt").focus()
            return;
        }

        var loginData = {
            grant_type: 'password',
            username: self.loginEmail(),
            password: self.loginPassword(),
            scope: privateOwnerId + ',' + dataOwnerId
        };

        freezUI();
        $.ajax({
            type: 'POST',
            crossOrigin: true,
            //beforeSend: function (request) {
            //    request.setRequestHeader("OwnerKey", privateOwnerId);
            //    request.setRequestHeader("Access-Control-Allow-Origin", "*");
            //},
            url: urls.loginUrl,
            data: loginData,
        }).done(function (data) {
            debugger
            self.user(data.userName);

            $.cookie("token", data.access_token, { path: '/' });
            $loginForm.data("kendoWindow").close();

            var retUrlObject = self.requestAppObject();
            if (retUrlObject)
                self.callApi(retUrlObject.url, retUrlObject.callType, {}, retUrlObject.callBackFunc);

            headerMenu.shouldShowLogout(true);
            unfreezUI();

        }).fail(showAjaxError);
    }

    self.logout = function () {

        self.user('');
        headerMenu.shouldShowLogout(false);

        $.removeCookie('token');
        //window.location = '/';
        self.openLogin();
        self.result('لطفا اطلاعات کاربری خود را وارد نمایید');
    }

    self.openLogin = function () {
        unfreezUI();
        $loginForm.data("kendoWindow").center();
        $loginForm.data("kendoWindow").open();
    }

    self.onRequestRestorePass = function () {
        if (self.loginEmail() !== "" && self.loginEmail() !== undefined) {
            $.ajax({
                type: "POST",
                url: urls.sendPassCodeUrl,
                data: { username: self.loginEmail() },
                dataType: "json",
                crossOrigin: true,
                beforeSend: function (request) {
                    request.setRequestHeader("OwnerKey", privateOwnerId);
                    request.setRequestHeader("DataOwnerKey", dataOwnerId);
                    request.setRequestHeader("DataOwnerCenterKey", dataOwnerCenterId);
                    request.setRequestHeader("Access-Control-Allow-Origin", "*");
                },
                success: function (response) {
                    self.showRestorePasswordDiv();
                    showSuccess('', 'کد بازیابی رمز عبور برای شما ارسال گردید');
                }
            }).fail(showAjaxError);
        }
        else {
            showError('', 'لطفا نام کاربری خود را وارد نمایید');
            $(".email-txt").focus()
        }
    };

    self.onRestorePass = function () {
        if (self.resetCode() === '' || self.newPass() === '' || self.confirmNewPass() === '') {
            showError('', 'لطفا اطلاعات درخواست شده را تکمیل  نمایید');
            $(".sms-reset-code").focus()
            return;
        }
        if (self.newPass() !== self.confirmNewPass()) {
            showError('', 'رمز عبور جدید با تکرار آن برابر نیست');
            return;
        }

        $.ajax({
            type: "POST",
            url: urls.resetPasswordByCodeUrl,
            data: { username: self.loginEmail(), password: self.newPass(), code: self.resetCode() },
            dataType: "json",
            crossOrigin: true,
            beforeSend: function (request) {
                request.setRequestHeader("OwnerKey", privateOwnerId);
                request.setRequestHeader("DataOwnerKey", dataOwnerId);
                request.setRequestHeader("DataOwnerCenterKey", dataOwnerCenterId);
                request.setRequestHeader("Access-Control-Allow-Origin", "*");
            },
            success: function (response) {

                self.hideRestorePasswordDiv();
                showSuccess('', 'رمز عبور شما با موفقیت تغییر کرد');
            }
        }).fail(showAjaxError);
    };

    self.onCancelRestorePass = function () {
        self.hideRestorePasswordDiv();
    };

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

    self.showRestorePasswordDiv = function () {
        self.clearRestorePasswordform();
        $(".restore-password-div").removeClass('hide').show();
        $(".login-div").hide();
        $(".k-window-title").html("بازیابی رمزعبور");
};

    self.hideRestorePasswordDiv = function () {
        $(".restore-password-div").hide();
        $(".login-div").show();
        $(".k-window-title").html("ورود");
        self.clearRestorePasswordform();
    };

    self.clearRestorePasswordform = function () {
        self.resetCode('');
        self.newPass('');
        self.confirmNewPass('');
    };
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
            requestEnd: onRequestEnd,
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
            resizable: true,
            sortable: true,
            filterable: {
                //mode: "row",
                extra: false,
                operators: {
                    string: {
                        startswith: "شروع با",
                        eq: "مساوی با",
                        neq: "نامساوی",
                        contains: "شامل"
                    }
                }
            },
            height: 650,
            toolbar: ["save", "cancel", "excel"],
            excel: {
                fileName: "مدیریت تامین کالا - کالاهای انبار.xlsx",
                filterable: true,
                allPages: true
            },
            columns: [
                { field: "rowNo", title: "#", width: 70, template: "#= renderNumber(data) #", filterable: false },
                { field: "productCode", title: "کد کالا", width: 100 },
                { field: "productName", title: "نام کالا", width: 200 },
                { field: "minQty", title: "حداقل موجودی", width: 100 },
                { field: "maxQty", title: "حداکثر موجودی", width: 100 },
                { field: "reorderLevel", title: "نقطه سفارش", width: 100 },
                { field: "reorderCalcTypeInfo", title: "شیوه سفارش", width: 180, editor: categoryDropDownEditor, template: "#=reorderCalcTypeInfo.reorderTypeName#" },
            ],
            editable: true,
            dataBinding: onDataBinding
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
//******************************************************************//

function reviewProductRequestViewModel() {
    var self = this;

    self.stockRequests = ko.observableArray([]);
    self.chosenStockProductRequest = ko.observable();
    self.storeRequestInfo = ko.observable();
    self.shouldShowRequestInfo = ko.observable(false);

    self.initStockRequestGrid = function () {
        var dataSource = new kendo.data.DataSource({
            transport: {
                read: {
                    url: urls.stockProductRequestUrl,
                    dataType: "json",
                    contentType: "application/json",
                    type: "POST",
                    beforeSend: gridAuthHeader
                },
                parameterMap: function (options, operation) {
                    if (operation == "read")
                        return kendo.stringify(options);
                }
            },
            pageSize: 20,
            requestEnd: onRequestEnd,
            schema: {
                model: {
                    id: "uniqueId",
                    fields: {
                        uniqueId: { editable: false, nullable: true },
                        stockName: { type: "string" },
                        requestNo: { type: "string" },
                        requestPDate: { type: "string" },
                        supplyType: { type: "string" },
                        requestType: { type: "string" },
                        supplyByStock: { type: "string" },
                        supplierName: { type: "string" }
                    }
                }
            }
        });

        $(".stock-requests-grid").kendoGrid({
            dataSource: dataSource,
            navigatable: true,
            pageable: true,
            sortable: true,
            resizable: true,
            selectable: 'row',
            filterable: {
                //mode: "row",
                extra: false,
                operators: {
                    string: {
                        startswith: "شروع با",
                        eq: "مساوی با",
                        neq: "نامساوی",
                        contains: "شامل"
                    }
                }
            },
            height: 350,
            change: onChange,
            toolbar: ["excel"],
            excel: {
                fileName: "مدیریت تامین کالا - بازنگری.xlsx",
                filterable: true,
                allPages: true,
            },
            columns: [
                { field: "rowNo", title: "#", width: 70, template: "#= renderNumber(data) #", filterable: false },
                { field: "stockName", title: "نام انبار", width: 120 },
                { field: "requestNo", title: "شماره درخواست", width: 150 },
                { field: "requestPDate", title: "تاریخ درخواست", width: 140 },
                { field: "supplyType", title: "شیوه تامین", width: 120 },
                { field: "requestType", title: "شیوه محاسبه", width: 120 },
                { field: "supplyByStock", title: "انبار تامین", width: 120 },
                { field: "supplierName", title: "تامین کننده", width: 120 },
            ],
            dataBinding: onDataBinding
        });
    };

    function onChange(arg) {
        var selectedItem = $(".stock-requests-grid").data("kendoGrid").dataItem(this.select());

        self.refreshStoreRequestInfo(selectedItem);
    }

    self.refreshStoreRequestInfo = function (data) {
        self.chosenStockProductRequest(data);
        self.initRequestReviewGrid();
        self.shouldShowRequestInfo(true);
    };

    self.initRequestReviewGrid = function () {

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
                        return kendo.stringify({ stockProductRequestProductList: options.models, stockId: options.stockId });
                }
            },
            batch: true,
            pageSize: 20,
            requestEnd: onRequestEnd,
            schema: {
                model: {
                    id: "uniqueId",
                    fields: {
                        uniqueId: { editable: false, nullable: true },
                        productCode: { type: "string", editable: false },
                        productName: { type: "string", editable: false },
                        requestQty: { type: "number", editable: false },
                        stockLevelQty: { type: "number", editable: false },
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
            resizable: true,
            filterable: {
                //mode: "row",
                extra: false,
                operators: {
                    string: {
                        startswith: "شروع با",
                        eq: "مساوی با",
                        neq: "نامساوی",
                        contains: "شامل"
                    }
                }
            },
            height: 650,
            detailTemplate: kendo.template($("#template").html()),
            detailInit: detailInit,
            dataBound: function () {
                // this.expandRow(this.tbody.find("tr.k-master-row").first());
            },
            toolbar: ["save", "cancel", "excel"],
            excel: {
                fileName: "مدیریت تامین کالا - بازنگری.xlsx",
                filterable: true,
                allPages: true,
            },
            columns: [
                { field: "rowNo", title: "#", width: 70, template: "#= renderNumber(data) #", filterable: false },
                { field: "productCode", title: "کد محصول", width: 90 },
                { field: "productName", title: "نام محصول", width: 180 },
                { field: "requestQty", title: "تعداد سفارش", width: 60 },
                { field: "stockLevelQty", title: "موجودی انبار", width: 100 },
                { field: "accepted1Qty", title: "نظر تاییدکننده اول", width: 100 },
                { field: "accepted2Qty", title: "نظر تاییدکننده دوم", width: 100 },
                { field: "accepted3Qty", title: "نظر تاییدکننده سوم", width: 100 },
                { field: "myAcceptedQty", title: "نظر من", width: 80 },
            ],
            editable: true,
            dataBinding: onDataBinding
        });
    };

    function detailInit(e) {
        var detailRow = e.detailRow;

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
            resizable: true,
            pageable: true,
            filterable: {
                //mode: "row",
                extra: false,
                operators: {
                    string: {
                        startswith: "شروع با",
                        eq: "مساوی با",
                        neq: "نامساوی",
                        contains: "شامل"
                    }
                }
            },
            columns: [
                { field: "rowNo", title: "#", width: 70, template: "#= renderNumber(data) #", filterable: false },
                { field: "stockProductRequestRuleName", title: "شماره قانون", width: "150px" },
                { field: "requestQty", title: "تعداد", width: "150px", filterable: false },
            ],
            dataBinding: onDataBinding
        });
    }

    self.initStockRequestGrid();

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
            requestEnd: onRequestEnd,
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
            toolbar: ["excel"],
            excel: {
                fileName: "مدیریت تامین کالا - سوابق.xlsx",
                filterable: true,
                allPages: true,
            },
            navigatable: true,
            pageable: true,
            resizable: true,
            sortable: true,
            filterable: {
                //mode: "row",
                extra: false,
                operators: {
                    string: {
                        startswith: "شروع با",
                        eq: "مساوی با",
                        neq: "نامساوی",
                        contains: "شامل"
                    }
                }
            },
            height: 650,
            detailTemplate: kendo.template($("#template").html()),
            detailInit: detailInit,
            columns: [
                { field: "rowNo", title: "#", width: 70, template: "#= renderNumber(data) #", filterable: false },
                { field: "sourceStockRequestNo", title: "شماره درخواست" },
                { field: "requestPDate", title: "تاریخ درخواست", filterable: false },
                { field: "acceptName1", title: "تایید کننده اول", filterable: false },
                { field: "acceptName2", title: "تایید کننده دوم", filterable: false },
                { field: "acceptName3", title: "تایید کننده سوم", filterable: false },
                { field: "stockProductRequestStatus", title: "وضعیت", filterable: false },
                { field: "sendToSourceStockDatePDate", title: "تاریخ ارسال", filterable: false },
                { field: "targetStockIssueDatePDate", title: "تاریخ تحویل", filterable: false },
            ],
            editable: false,
            dataBinding: onDataBinding
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
            resizable: true,
            pageable: true,
            filterable: {
                //mode: "row",
                extra: false,
                operators: {
                    string: {
                        startswith: "شروع با",
                        eq: "مساوی با",
                        neq: "نامساوی",
                        contains: "شامل"
                    }
                }
            },
            columns: [
                { field: "rowNo", title: "#", width: 70, template: "#= renderNumber(data) #", filterable: false },
                { field: "productCode", title: "کد کالا", width: "70px" },
                { field: "productName", title: "نام کالا", width: "150px" },
                { field: "accepted1Qty", title: "تعداد تایید شده اول", width: "150px", filterable: false },
                { field: "accepted2Qty", title: "تعداد تایید شده دوم", width: "150px", filterable: false },
                { field: "accepted3Qty", title: "تعداد تایید شده سوم", width: "150px", filterable: false },
                { field: "requestQty", title: "تعداد ارسال شده", width: "150px", filterable: false },
                { field: "deliveredQty", title: "تعداد تحویل شده", width: "150px", filterable: false },
            ],
            dataBinding: onDataBinding
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

        accountManagerApp.callApi(urls.permissionCatalogsOfUserUrl, 'POST', { userId: self.chosenUser().id }, function (data) {
            data.forEach(function (itm) {
                if (itm.grant)
                    $('input[name="grants[]"][data-id="' + itm.permissionCatalogId + '"]').prop("checked", true);
            });
        });
    }, self);

    self.refreshPermissions = function () {
        accountManagerApp.callApi(urls.permissionCatalogsUrl, 'POST', {}, function (data) {
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

        accountManagerApp.callApi(urls.savePermissionCatalogsUrl, 'POST', {
            data: JSON.stringify({
                userId: self.chosenUser().id, permissionCatalogs: _permissions
            })
        }, function (data) {
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
            requestEnd: onRequestEnd,
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
            resizable: true,
            sortable: true,
            filterable: {
                //mode: "row",
                extra: false,
                operators: {
                    string: {
                        startswith: "شروع با",
                        eq: "مساوی با",
                        neq: "نامساوی",
                        contains: "شامل"
                    }
                }
            },
            height: 650,
            toolbar: ["save", "cancel", "excel"],
            excel: {
                fileName: "مدیریت تامین کالا - انبارها.xlsx",
                filterable: true,
                allPages: true,
            },
            columns: [
                { field: "rowNo", title: "#", width: 70, template: "#= renderNumber(data) #", filterable: false },
                { field: "stockCode", title: "کد انبار", width: 100 },
                { field: "stockName", title: "نام انبار", width: 150 },
                { field: "approver1", title: "تایید کننده اول", width: "150px", editor: approverDropDownEditor, template: "#=approver1.userName#", filterable: false },
                { field: "approver2", title: "تایید کننده دوم", width: "150px", editor: approverDropDownEditor, template: "#=approver2.userName#", filterable: false },
                { field: "approver3", title: "تایید کننده سوم", width: "150px", editor: approverDropDownEditor, template: "#=approver3.userName#", filterable: false },
                { field: "stockType", title: "نوع انبار", width: "150px", editor: stockTypeDropDownEditor, template: "#=stockType.stockTypeName#", filterable: false },
                { field: "mainStock", title: "انباراصلی", width: "150px", editor: nestedStockDropDownEditor, template: "#=mainStock.stockName#", filterable: false },
                { field: "relatedStock", title: "تامین کننده", width: "150px", editor: nestedStockDropDownEditor, template: "#=relatedStock.stockName#", filterable: false },
            ],
            editable: true,
            dataBinding: onDataBinding
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
            requestEnd: onRequestEnd,
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
            resizable: true,
            sortable: true,
            filterable: {
                //mode: "row",
                extra: false,
                operators: {
                    string: {
                        startswith: "شروع با",
                        eq: "مساوی با",
                        neq: "نامساوی",
                        contains: "شامل"
                    }
                }
            },
            pageable: true,
            toolbar: kendo.template($("#toolbar-template").html()),
            height: 500,
            columns: [
                { field: "rowNo", title: "#", width: 70, template: "#= renderNumber(data) #", filterable: false },
                { field: "stockProductRequestRuleName", title: "نام", width: 100 },
                { field: "fromPDate", title: "از تاریخ", width: 150, filterable: false },
                { field: "topDate", title: "تا تاریخ", width: 150, filterable: false },
                { command: { text: "ویرایش", click: self.showEdit }, title: " ", width: "180px" }
            ],
            editable: false,
            dataBinding: onDataBinding
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

    self.selectedSupplierId = ko.observable('');
    self.refreshSuppliers = function () {
        var supValue = $("#suppliers").val().replace("ی", "ي").replace("ک", "ك")
        $("#suppliers").val(supValue);
        if (supValue.length < 3) return;
        $("#suppliers").kendoAutoComplete({
            dataTextField: "supplierName",
            minLength: 3,
            delay: 500,
            dataSource: {
                transport: {
                    read: {
                        url: urls.filterSuppliersUrl,
                        dataType: "json",
                        contentType: "application/json",
                        type: "Post",
                        beforeSend: gridAuthHeader,
                    },
                    parameterMap: function (options, operation) {
                        options.searchTerm = $("#suppliers").val();

                        if (operation == "read")
                            return kendo.stringify(options);
                    }
                }
            },
            select: function (e) {
                var dataItem = this.dataItem(e.item.index());

                self.selectedSupplierId(dataItem.uniqueId);
            }
        });
    }

    self.selectedProductId = ko.observable('');
    self.refreshProduct = function () {
        var proValue = $("#product").val().replace("ی", "ي").replace("ک", "ك")
        $("#product").val(proValue);
        if (proValue.length < 3) return;

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

    self.selectedMainProductGroupId = ko.observable('');
    self.refreshMainProductGroup = function () {
        var proGroupValue = $("#main-product-group").val().replace("ی", "ي").replace("ک", "ك")
        $("#main-product-group").val(proGroupValue);
        if (proGroupValue.length < 3) return;
        $("#main-product-group").kendoAutoComplete({
            dataTextField: "groupName",
            minLength: 3,
            delay: 500,
            dataSource: {
                transport: {
                    read: {
                        url: urls.filterMainProductGroupsUrl,
                        dataType: "json",
                        contentType: "application/json",
                        type: "POST",
                        beforeSend: gridAuthHeader,
                    },
                    parameterMap: function (options, operation) {
                        options.searchTerm = $("#main-product-group").val();

                        if (operation == "read")
                            return kendo.stringify(options);
                    }
                }
            },
            select: function (e) {
                var dataItem = this.dataItem(e.item.index());

                self.selectedMainProductGroupId(dataItem.uniqueId);
            }
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

                    self.selectedSupplierId(data.supplierId);
                    $('#suppliers').val(data.supplierName);

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

                    self.selectedMainProductGroupId(data.mainProductGroupId);
                    $('#main-product-group').val(data.mainProductGroupName);


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

    $(".product-rule-edit-page").on("keyup", "#main-product-group", function () {
        self.refreshMainProductGroup();
    });

    $(".product-rule-edit-page").on("keyup", "#product", function () {
        self.refreshProduct();
    });

    $(".product-rule-edit-page").on("keyup", "#suppliers", function () {
        self.refreshSuppliers();
    });

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
            data["supplierId"] = self.selectedSupplierId();
        if (viaFilter === "2")
            data["productId"] = self.selectedProductId();
        if (viaFilter === "3")
            data["mainProductGroupId"] = self.selectedMainProductGroupId();

        var errors = [];
        if (data.ruleName === '')
            errors.push('نام را وارد نمایید');
        if (data.fromDate === '' || data.toDate === '')
            errors.push('تاریخ را انتخاب نمایید');
        if (viaFilter === "0")
            errors.push('لطفا یکی از موارد فیلد براساس را انتخاب نمایید')
        if (viaFilter === "1" && data["supplierId"] === '')
            errors.push('تامین کننده را انتخاب نمایید');
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

        //console.log(JSON.stringify(data));

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
                    beforeSend: gridAuthHeader,
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
            requestEnd: onRequestEnd,
            schema: {
                model: {
                    id: "uniqueId",
                    fields: {
                        uniqueId: { editable: false, nullable: true },
                        productCode: { editable: false },
                        storeProductName: { editable: false },
                        productTypeInfo: { defaultValue: { uniqueId: "", productTypeName: "" } },
                        mainSupplierName: { editable: false },
                        manufactureName: { editable: false },
                        isActiveInOrder: { editable: true },
                        barcode: { editable: false }
                    }
                }
            }
        });

        $(".products-grid").kendoGrid({
            dataSource: dataSource,
            navigatable: true,
            resizable: true,
            pageable: true,
            sortable: true,
            filterable: {
                //mode: "row",
                extra: false,
                operators: {
                    string: {
                        startswith: "شروع با",
                        eq: "مساوی با",
                        neq: "نامساوی",
                        contains: "شامل"
                    }
                }
            },
            height: 650,
            toolbar: ["save", "cancel", "excel"],
            excel: {
                fileName: "مدیریت تامین کالا - کالاها.xlsx",
                filterable: true,
                allPages: true,
            },
            columns: [
                { field: "rowNo", title: "#", width: 70, template: "#= renderNumber(data) #", filterable: false, },
                { field: "productCode", title: "کد کالا", width: 100 },
                { field: "storeProductName", title: "نام کالا", width: 200 },
                {
                    field: "isActiveInOrder", title: "فعال", width: 100, filterable: false,
                    template: "<div class='chk-grid'><input name='isActiveInOrder' class='chk-isActiveInOrder' type='checkbox' data-bind='checked: isActiveInOrder' #= isActiveInOrder ? checked='checked' : '' #/></div>"
                },
                { field: "productTypeInfo", title: "نوع کالا", width: "180px", editor: categoryDropDownEditor, template: "#=productTypeInfo.productTypeName#" },
                { field: "mainSupplierName", title: "تامین کننده", width: 200 },
                { field: "manufactureName", title: "تولید کننده", width: 200 },
                { field: "barcode", title: "بارکد", width: 200 },
            ],
            editable: true,
            dataBinding: onDataBinding
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
        };

        var grid = $(".products-grid").data("kendoGrid");
        grid.tbody.on("change", ".chk-isActiveInOrder", function (e) {
            e.preventDefault();

            var row = $(e.target).closest("tr");

            var item = grid.dataItem(row);

            item.set("isActiveInOrder", $(e.target).is(":checked") ? true : false);
        });
    };

    self.initGrid();
};
//******************************************************************//

function userManagementViewModel() {
    var self = this;

    self.initUserManagementGrid = function (id) {
        var dataSource = new kendo.data.DataSource({
            transport: {
                read: {
                    url: urls.usersUrl,
                    dataType: "json",
                    contentType: "application/json",
                    type: "Get",
                    beforeSend: gridAuthHeader
                },
                parameterMap: function (options, operation) {
                    if (operation == "read")
                        return kendo.stringify(options);
                }
            },
            batch: true,
            pageSize: 20,
            requestEnd: onRequestEnd,
            schema: {
                model: {
                    id: "id",
                    fields: {
                        id: { editable: false, nullable: true },
                        userName: { editable: false },
                        email: { editable: false },
                        mobile: { editable: false },
                    }
                }
            }
        });

        $(".user-management-grid").kendoGrid({
            dataSource: dataSource,
            navigatable: true,
            resizable: true,
            sortable: true,
            filterable: {
                //mode: "row",
                extra: false,
                operators: {
                    string: {
                        startswith: "شروع با",
                        eq: "مساوی با",
                        neq: "نامساوی",
                        contains: "شامل"
                    }
                }
            },
            pageable: true,
            toolbar: kendo.template($("#toolbar-template").html()),
            height: 500,
            columns: [
                { field: "rowNo", title: "#", width: 70, template: "#= renderNumber(data) #", filterable: false, },
                { field: "userName", title: "نام کاربری", width: 200 },
                { field: "email", title: "ایمیل", width: 250 },
                { field: "mobile", title: "شماره تلفن", width: 150 },
                { command: { text: "ویرایش", click: self.showEdit }, title: " ", width: "180px" }
            ],
            editable: false,
            dataBinding: onDataBinding
        });
    };

    self.addUser = function () {
        self.openWindow("/userManager/edit");
    }

    self.openWindow = function (url) {
        $('.user-management-page').append('<div class="edit-window"></div>');

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
        $('.user-management-grid').data('kendoGrid').dataSource.read();
        $('.user-management-grid').data('kendoGrid').refresh();
    }

    self.showEdit = function (e) {
        e.preventDefault();

        var dataItem = this.dataItem($(e.currentTarget).closest("tr"));

        self.openWindow("/userManager/edit?id=" + dataItem.id);
    }

    self.initUserManagementGrid();
};

function userEditManagerViewModel() {
    var self = this;

    self.fullName = ko.observable('');
    self.userName = ko.observable('');
    self.password = ko.observable('');
    self.confirmPassword = ko.observable('');
    self.email = ko.observable('');
    self.mobile = ko.observable('');

    self.checkEditMode = function () {

        freezUI();
        var id = $('#userId').val();
        if (id)
            accountManagerApp.callApi(urls.userUrl, "POST", { userId: id }, function (data) {

                self.fullName(data.fullName);
                self.userName(data.userName);
                self.email(data.email);
                self.mobile(data.mobile);

                $('#userName').attr('disabled', '');
                $('#email').attr('disabled', '');

                unfreezUI();
            });
        else
            unfreezUI();

    };

    self.checkEditMode();

    $(".user-edit-page").on("click", '.btn-cancel', function (e) {
        e.preventDefault;
        $(".edit-window").data("kendoWindow").close();
    });

    $(".user-edit-page").on("click", '.btn-save', function (e) {
        e.preventDefault;

        var data = {
            uniqueId: $('#userId').val(),
            userId: $('#userId').val(),
            fullName: self.fullName(),
            userName: self.userName(),
            password: self.password(),
            confirmPassword: self.confirmPassword(),
            email: self.email(),
            mobile: self.mobile(),
        };

        var valid = false;

        if (data.userId && data.userId != '')
            valid = self.validateUpdateModel(data);
        else
            valid = self.validateCreateModel(data);

        if (valid)
            self.validateEmail(data.email, data.userId, function (isValidEmail) {
                if (isValidEmail) {
                    accountManagerApp.callApi(urls.saveUserUrl, 'POST', { user: JSON.stringify(data) }, function (data) {
                        $(".edit-window").data("kendoWindow").close();
                    });
                }
                else
                    showError('', 'ایمیل وارد شده تکراری است');
            })
    });

    self.validateCreateModel = function (data) {
        var errors = [];
        if (data.userName === '')
            errors.push('نام کاربری را وارد نمایید');
        if (data.password === '')
            errors.push('رمزعبور را وارد نمایید');
        if (data.confirmPassword === '')
            errors.push('تکرار رمز را وارد نمایید');
        if (data.email === '')
            errors.push('ایمیل را وارد نمایید');
        if (data.mobile === '')
            errors.push('موبایل را وارد نمایید');
        if (data.password !== data.confirmPassword)
            errors.push('رمز و تکرار آن باید یکسان باشند');

        if (data.password.length < 4)
            errors.push('رمزعبور باید حداقل 4 کاراکتر باشد');


        //todo: email, user name, mobile should be unique for this appId

        if (errors.length > 0) {
            errors.forEach(function (err) {
                showError('', err);
            });
            return false;
        }

        return true;
    }

    self.validateUpdateModel = function (data) {
        var errors = [];
        if (data.userName === '')
            errors.push('نام کاربری را وارد نمایید');

        if (data.email === '')
            errors.push('ایمیل را وارد نمایید');

        if (data.mobile === '')
            errors.push('موبایل را وارد نمایید');

        if (data.password !== data.confirmPassword)
            errors.push('رمز و تکرار آن باید یکسان باشند');

        if (data.password.length < 4)
            errors.push('رمزعبور باید حداقل 4 کاراکتر باشد');

        if (errors.length > 0) {
            errors.forEach(function (err) {
                showError('', err);
            });
            return false;
        }

        return true;
    }

    self.validateEmail = function (email, userid, callback) {
        accountManagerApp.callApi(urls.checkUserEmail, 'POST', { email: email, userid: userid }, callback);
    }
};
//******************************************************************//

function changePasswordViewModel() {
    // Data
    var self = this;

    self.oldPass = ko.observable('');
    self.newPass = ko.observable('');
    self.confirmNewPass = ko.observable('');

    self.onChangePass = function () {
        if (self.oldPass() === '' || self.newPass() === '' || self.confirmNewPass() === '') {
            showError('', 'لطفا اطلاعات درخواستی را تکمیل نمایید');
            $(".old-pass").focus();
            return;
        }
        if (self.newPass() !== self.confirmNewPass()) {
            showError('', 'رمز عبور جدید با تکرار آن برابر نیست');
            $(".new-pass").focus();
            return;
        }

        accountManagerApp.callApi(urls.changePasswordUrl, "POST", {
            oldPassword: self.oldPass(), newPassword: self.newPass(), confirmPassword: self.confirmNewPass
        }, function (data) {
            showSuccess('', 'رمز عبور شما تغییر کرد');
            self.clearForm();
        });
    };

    self.clearForm = function () {
        self.oldPass('');
        self.newPass('');
        self.confirmNewPass('');
    };
};
