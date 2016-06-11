function appsViewModel() {
    var self = this;

    self.appName = ko.observable();
    self.appId = ko.observable();
    self.showAppDialog = ko.observable(false);

    self.addApp = function () {
        unfreezUI();
        self.showAppDialog(true);
        $('#appName').focus();
    };

    self.editApp = function (e) {
        var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
        self.appId(dataItem.id);
        self.appName(dataItem.name);
        self.showAppDialog(true);
        $('#appName').focus();
    }

    self.removeApp = function (e) {
        var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
        if (confirm('Are you sure for deleting ' + dataItem.name + ' app?')) {
            accountManagerApp.callApi(urls.appsRemoveUrl + '/' + dataItem.id, "POST", {}, function (data) {
                $('#app-management-grid').data('kendoGrid').dataSource.read();
                $('#app-management-grid').data('kendoGrid').refresh();
            });
        }
    };

    self.appCancel = function () {
        self.appName('');
        self.appId('');
        self.showAppDialog(false);
    };

    self.appSave = function () {
        console.log('appSave');
        var app = {
            Id: self.appId(),
            Name: self.appName()
        };

        if (!app.Name || $.trim(app.Name).length == 0) {
            showError('لطفا نام برنامه را وارد کنید.');
            $('#appName').focus();
            return;
        }

        accountManagerApp.callApi(urls.appsSaveUrl, "POST", app, function (data) {
            self.appName('');
            self.appId('');
            self.showAppDialog(false);
            $('#app-management-grid').data('kendoGrid').dataSource.read();
            $('#app-management-grid').data('kendoGrid').refresh();
        });
    };

    self.apps = ko.observableArray();

    self.initAppManagementGrid = function () {

        var dataSource = new kendo.data.DataSource({
            transport: {
                read: {
                    url: urls.appsUrl,
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
                        id: { editable: false, nullable: false },
                        name: { editable: false }
                    }
                }
            }
        });

        $("#app-management-grid").kendoGrid({
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
                { field: "id", title: "Id", width: 200 },
                { field: "name", title: "Name", width: 250 },
                { command: { text: "Edit", click: self.editApp }, title: " ", width: "180px" },
                { command: { text: "Remove", click: self.removeApp }, title: " ", width: "180px" }
            ],
            editable: false,
            dataBinding: onDataBinding
        });
    }

    self.initAppManagementGrid();
}