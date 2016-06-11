'use strict';
function catalogViewModel() {
    var self = this;
    self.showPermissionDialog = ko.observable(false);
    self.selectedCatalog = ko.observable();
    self.permissionName = ko.observable();
    self.permissionId = ko.observable();

    self.appList = ko.observableArray();
    self.selectedApp = ko.observable();
    self.selectedApp.subscribe(function (e) {
        self.moduleList(e.modules);
    });

    self.moduleList = ko.observableArray();
    self.selectedModule = ko.observable();
    self.selectedModule.subscribe(function (e) {
        self.resourceList(e.resources);
    });

    self.resourceList = ko.observableArray();
    self.selectedResource = ko.observable();
    self.selectedResource.subscribe(function (e) {

    });

    self.actionList = ko.observableArray();
    self.selectedAction = ko.observable();
    self.selectedAction.subscribe(function (e) {

    });

    self.permissionCancel = function () {
        self.permissionName('');
        self.permissionId('');
        self.showPermissionDialog(false);
    };

    self.permissionSave = function () {
        var permission = null;

        if (!self.selectedApp()) {
            showError('لطفا یک برنامه را انتخاب کنید.');
            return;
        }

        if (!self.selectedModule()) {
            showError('لطفا یک ماژول را انتخاب کنید.');
            return;
        }

        if (!self.selectedResource()) {
            showError('لطفا یک منبع را انتخاب کنید.');
            return;
        }

        if (!self.selectedAction()) {
            showError('لطفا یک اکشن را انتخاب کنید.');
            return;
        }

        if (self.permissionName() == null || self.permissionName().length == 0) {
            showError('لطفا یک نام برای مجوز انتخاب کنید.');
            return;
        }

        var permission = {
            permissionId: self.permissionId(),
            permissionName: self.permissionName(),
            resourceId: self.selectedResource().resourceId,
            actionId: self.selectedAction().actionId
        };

        accountManagerApp.callApi(urls.permissionsSaveUrl, "POST", permission, function (data) {
            var grid = $("#all-permissions-grid").data("kendoGrid");
            grid.dataSource.read();
            grid.refresh();

            // close popup
            self.permissionCancel();

        }, function (err) {
            if (err.status == 409 /* conflict */)
                showError('در حال حاضر برای منبع و اکشن انتخاب شده مجوز تعیین شده است.');
        });

    };

    self.addPermission = function () {
        self.showPermissionDialog(true);
    };

    self.editPermission = function (e) {
        var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
        var internalSelectedApp = ko.utils.arrayFirst(self.appList(), function (app) {
            return app.applicationId === dataItem.applicationId;
        });

        if (internalSelectedApp)
            self.selectedApp(internalSelectedApp);
        else
            self.selectedApp({});

        var internalSelectedModule = ko.utils.arrayFirst(self.moduleList(), function (module) {
            return module.moduleId === dataItem.moduleId;
        });

        if (internalSelectedModule)
            self.selectedModule(internalSelectedModule);
        else
            self.selectedModule({});

        var internalSelectedResource = ko.utils.arrayFirst(self.resourceList(), function (resource) {
            return resource.resourceId === dataItem.resourceId;
        });

        if (internalSelectedResource)
            self.selectedResource(internalSelectedResource);
        else
            self.selectedResource({});

        var internalSelectedAction = ko.utils.arrayFirst(self.actionList(), function (action) {
            return action.actionId == dataItem.actionId;
        });
        if (internalSelectedAction)
            self.selectedAction(internalSelectedAction);
        else
            self.selectedAction({});

        self.permissionName(dataItem.permissionName);
        self.permissionId(dataItem.permissionId);
        self.showPermissionDialog(true);
    };

    self.removePermission = function (e) {
        var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
        if (confirm('آیا برای حذف مجوز ' + dataItem.permissionName + ' مطمئن هستید؟')) {
            accountManagerApp.callApi(urls.permissionRemoveUrl + '/' + dataItem.permissionId, "POST", {}, function (data) {
                var grid = $("#all-permissions-grid").data("kendoGrid");
                grid.dataSource.read();
                grid.refresh();

                // close popup
                self.permissionCancel();
            });
        }
    };

    self.addToCatalog = function () {
        if (!self.selectedCatalog()) {
            showError('لطفا یک کاتالوگ انتخاب کنید.');
            return;
        }

        var allPermissionsGrid = $("#all-permissions-grid").data("kendoGrid");
        var internalSelectedPermission = allPermissionsGrid.dataItem(allPermissionsGrid.select());
        accountManagerApp.callApi(
            urls.permissionToCatalogUrl + '/' + self.selectedCatalog().id + '/' + internalSelectedPermission.permissionId,
            "POST", {}, function (data) {
                var selectedCatalogGrid = $("#selected-catalog-permissions-grid").data("kendoGrid");
                selectedCatalogGrid.dataSource.read();
                selectedCatalogGrid.refresh();
        });
    };

    self.removeFromCatalog = function (e) {
        if (!self.selectedCatalog()) {
            showError('لطفا یک کاتالوگ انتخاب کنید.');
            return;
        }

        var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
        if (confirm('آیا برای حذف مجوز ' + dataItem.permissionName + ' از کاتالوگ ' + self.selectedCatalog().title + ' مطمئن هستید؟')) {

            accountManagerApp.callApi(urls.permissionRemoveFromCatalogUrl + '/' + self.selectedCatalog().id + '/' + dataItem.permissionId, "POST", {}, function (data) {
                var grid = $("#selected-catalog-permissions-grid").data("kendoGrid");
                grid.dataSource.read();
                grid.refresh();

                // close popup
                self.permissionCancel();
            });
        }
    };

    function makeTree(nodes) {
        var map = {}, node, roots = [];
        for (var i = 0; i < nodes.length; i += 1) {
            node = nodes[i];
            node.items = [];
            map[node.id] = i; // use map to look-up the parents
            if (node.parent !== "" && node.parent != null) {
                var x = map[node.parent];
                if (x)
                    nodes[x].items.push(node);
            } else {
                roots.push(node);
            }
        }

        return roots;
    }

    function onCatalogSelected(e) {
        self.selectedCatalog(e.sender.dataItem(e.node));
        var grid = $("#selected-catalog-permissions-grid").data("kendoGrid");
        grid.dataSource.transport.options.read.url = urls.allPermissionsOfCatalogUrl + '/' + self.selectedCatalog().id;
        grid.dataSource.read();
        grid.refresh();
    }

    function initUI() {
        $('table').on('click', '.clickable-row', function (event) {
            $(this).addClass('active').siblings().removeClass('active');
        });

        // initials tree view
        accountManagerApp.callApi(urls.permissionCatalogsUrl, "POST", {}, function (data) {
            if (data != "") {

                data = makeTree(data);

                var catalogs = new kendo.data.HierarchicalDataSource({
                    data: data,
                    schema: {
                        model: {
                            children: "items"
                        }
                    }
                });

                $("#permission-catalogs-tree").kendoTreeView({
                    dataSource: catalogs,
                    dataTextField: ["title"],
                    select: onCatalogSelected
                });
                $('#permission-catalogs-tree').data('kendoTreeView').expand('.k-item');
            }

        });

        // initials all permissions
        var allPermissionsDataSource = new kendo.data.DataSource({
            transport: {
                read: {
                    url: urls.allPermissionsUrl,
                    dataType: "json",
                    contentType: "application/json",
                    type: "GET",
                    beforeSend: gridAuthHeader
                },
                parameterMap: function (options, operation) {
                    if (operation == "read")
                        return kendo.stringify(options);
                }
            },
            batch: true,
            pageSize: 10,
            requestEnd: onRequestEnd,
            schema: {
                model: {
                    id: "permissionId",
                    fields: {
                        permissionId: { editable: false, nullable: false },
                        permissionName: { editable: false, nullable: false },
                        resourceId: { editable: false, nullable: false },
                        resourceName: { editable: false, nullable: false },
                        moduleId: { editable: false, nullable: false },
                        moduleName: { editable: false, nullable: false },
                        applicationId: { editable: false, nullable: false },
                        applicationName: { editable: false, nullable: false },
                    }
                }
            }
        });
        $("#all-permissions-grid").kendoGrid({
            dataSource: allPermissionsDataSource,
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
            toolbar: kendo.template($("#all-permissions-toolbar-template").html()),
            height: 500,
            selectable: 'row',
            columns: [
                { field: "applicationName", title: "Application", width: 100 },
                { field: "moduleName", title: "Module", width: 100 },
                { field: "resourceName", title: "Resource", width: 100 },
                { field: "actionName", title: "Action", width: 100 },
                { field: "permissionName", title: "Permission", width: 100 },
                { command: { text: "Edit", click: self.editPermission }, title: " ", width: "100px" },
                { command: { text: "Remove", click: self.removePermission }, title: " ", width: "100px" }
            ],
            editable: false,
            dataBinding: onDataBinding
        });

        // initials selected catalogs permissions
        // toolbar: kendo.template($("#selected-catalog-permissions-toolbar-template").html()),
        var selectedCatalogPermissionsDataSource = new kendo.data.DataSource({
            transport: {
                read: {
                    dataType: "json",
                    contentType: "application/json",
                    type: "GET",
                    beforeSend: gridAuthHeader
                },
                parameterMap: function (options, operation) {
                    if (operation == "read")
                        return kendo.stringify(options);
                }
            },
            batch: true,
            pageSize: 10,
            requestEnd: onRequestEnd,
            schema: {
                model: {
                    id: "permissionId",
                    fields: {
                        permissionId: { editable: false, nullable: false },
                        permissionName: { editable: false, nullable: false },
                        resourceId: { editable: false, nullable: false },
                        resourceName: { editable: false, nullable: false },
                        moduleId: { editable: false, nullable: false },
                        moduleName: { editable: false, nullable: false },
                        applicationId: { editable: false, nullable: false },
                        applicationName: { editable: false, nullable: false },
                    }
                }
            }
        });
        $("#selected-catalog-permissions-grid").kendoGrid({
            dataSource: selectedCatalogPermissionsDataSource,
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
            height: 500,
            selectable: 'row',
            columns: [
                { field: "applicationName", title: "Application", width: 100 },
                { field: "moduleName", title: "Module", width: 100 },
                { field: "resourceName", title: "Resource", width: 100 },
                { field: "actionName", title: "Action", width: 100 },
                { field: "permissionName", title: "Permission", width: 100 },
                { command: { text: "Remove", click: self.removeFromCatalog }, title: " ", width: "180px" }
            ],
            editable: false,
            dataBinding: onDataBinding
        });


        // initials action-popup dropdowns
        accountManagerApp.callApi(urls.appsWithModulesUrl, "GET", {}, function (data) {
            self.appList(data);

            if (data.length > 0) {
                self.selectedApp(data[0]);
            }
        });

        //permissionActionsUrl
        accountManagerApp.callApi(urls.permissionActionsUrl, "GET", {}, function (data) {
            self.actionList(data);

            if (data.length > 0) {
                self.selectedAction(data[0]);
            }
        });

    }

    initUI();

}
