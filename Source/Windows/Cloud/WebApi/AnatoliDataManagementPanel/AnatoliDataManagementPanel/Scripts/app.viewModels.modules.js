function modulesViewModel() {
    var self = this;
    self.dataSource = null;
    self.appList = ko.observableArray();
    self.selectedApp = ko.observable();
    self.moduleId = ko.observable();
    self.moduleName = ko.observable();
    self.showModuleDialog = ko.observable(false);

    self.addModule = function () {
        console.log("addModule()");
        unfreezUI();
        self.showModuleDialog(true);
        $('#ModuleName').focus();
    };

    self.editModule = function (e) {
        var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
        var selectedAppItem = $.grep(self.appList(), function (a) { return a.id == dataItem.appId; })[0];
        if (selectedAppItem)
            self.selectedApp(selectedAppItem);
        self.moduleId(dataItem.id);
        self.moduleName(dataItem.moduleName);
        self.showModuleDialog(true);
        $('#moduleName').focus();
        // console.log('edit module:' + dataItem.name);
    }

    self.removeModule = function (e) {
        var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
        if (confirm('Are you sure for deleting ' + dataItem.moduleName + ' module?')) {
            accountManagerApp.callApi(urls.modulesRemoveUrl + '/' + dataItem.id, "POST", {}, function (data) {
                //self.initModuleManagementGrid();
                self.dataSource.remove(dataItem)

                //self.dataSource.sync();
            });
        }
    };

    self.moduleCancel = function () {
        self.moduleName('');
        self.moduleId('');
        self.showModuleDialog(false);
    };

    self.moduleSave = function () {
        console.log('moduleSave');

        if (self.selectedApp() == null || self.selectedApp() == {}) {
            showError('لطفا یک برنامه برای ماژول انتخاب کنید.');
            return;
        }

        var module = {
            appId: self.selectedApp().id,
            id: self.moduleId(),
            moduleName: self.moduleName()
        };

        if (module.appId == null || module.appId == '') {
            showError('لطفا یک برنامه برای ماژول انتخاب کنید.');
            return;
        }

        if (!module.moduleName || $.trim(module.moduleName).length == 0) {
            showError('لطفا نام ماژول را وارد کنید.');
            $('#moduleName').focus();
            return;
        }

        accountManagerApp.callApi(urls.modulesSaveUrl, "POST", module, function (data) {
            self.moduleName('');
            self.moduleId('');
            self.showModuleDialog(false);
            
            $('#module-management-grid').data('kendoGrid').dataSource.read();
            $('#module-management-grid').data('kendoGrid').refresh();
        });
    };

    self.modules = ko.observableArray();

    self.initModuleManagementGrid = function () {

        accountManagerApp.callApi(urls.appsUrl, "GET", {}, function (data) {
            self.appList(data);
            if (data.length > 0)
                self.selectedApp(data[0]);
        });

        self.dataSource = new kendo.data.DataSource({
            transport: {
                read: {
                    url: urls.modulesUrl,
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
                        appId: { editable: false, nullable: false },
                        appName: { editable: false, nullable: false },
                        id: { editable: false, nullable: false },
                        moduleName: { editable: false, nullable: false }
                    }
                }
            }
        });

        //if ($("#module-management-grid").data("kendoGrid")) {
        //    $("#module-management-grid").data("kendoGrid").destroy();
        //    $("#module-management-grid").empty();
        //}

        $("#module-management-grid").kendoGrid({
            dataSource: self.dataSource,
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
                { field: "appId", title: "App Id", width: 150 },
                { field: "appName", title: "App Name", width: 200 },
                { field: "id", title: "Id", width: 150 },
                { field: "moduleName", title: "Module Name", width: 200 },
                { command: { text: "Edit", click: self.editModule }, title: " ", width: "120px" },
                { command: { text: "Remove", click: self.removeModule }, title: " ", width: "120px" }
            ],
            editable: false,
            dataBinding: onDataBinding
        });
    }

    self.initModuleManagementGrid();
}