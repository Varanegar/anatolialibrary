var errorMessage = {
    unAuthorized: "شما مجوز لازم را برای این درخواست ندارید!",
},
gridAuthHeader = function (req) {
    var tokenKey = 'accessToken',
        token = $.cookie("token");
    req.setRequestHeader('Authorization', 'Bearer ' + token);
    req.setRequestHeader('OwnerKey', privateOwnerId);
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
            else if (jqXHR.status == 409) {
                showError('در حال حاضر آیتمی با این مشخصات وجود دارد.');
            }

            unfreezUI();
        });
    }

    var $loginForm = $(".login-form");
    self.login = function () {
        debugger;
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