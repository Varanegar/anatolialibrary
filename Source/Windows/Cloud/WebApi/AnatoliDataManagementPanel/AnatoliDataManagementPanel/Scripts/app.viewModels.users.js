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
        }).data("kendoWindow").center().maximize().open();
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