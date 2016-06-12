function resourcesViewModel() {
	var self = this;
	self.dataSource = null;
	self.appList = ko.observableArray();
	self.moduleList = ko.observableArray();
	self.selectedApp = ko.observable();
	self.selectedApp.subscribe(function (e) {
		self.moduleList(e.modules);
	});

	self.selectedModule = ko.observable();
	self.resourceId = ko.observable();
	self.resourceName = ko.observable();
	self.showResourceDialog = ko.observable(false);

	self.addResource = function () {
		unfreezUI();
		self.showResourceDialog(true);
		$('#ResourceName').focus();
	};

	self.editResource = function (e) {

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

		self.resourceId(dataItem.resourceId);
		self.resourceName(dataItem.resourceName);

		unfreezUI();
		self.showResourceDialog(true);
		$('#ResourceName').focus();
	}

	self.removeResource = function (e) {
		var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
		if (confirm('Are you sure for deleting ' + dataItem.resourceName + ' resource?')) {
			accountManagerApp.callApi(urls.resourcesRemoveUrl + '/' + dataItem.resourceId, "POST", {}, function (data) {

				self.dataSource.remove(dataItem)
				$('#resource-management-grid').data('kendoGrid').dataSource.read();
				$('#resource-management-grid').data('kendoGrid').refresh();
			});
		}
	};

	self.resourceCancel = function () {
		self.resourceName('');
		self.resourceId('');
		self.showResourceDialog(false);
	};

	self.resourceSave = function () {

		if (self.selectedApp() == null || self.selectedApp() == '' || self.selectedApp() == {}) {
			showError('لطفا یک برنامه برای این منبع انتخاب کنید.');
			return;
		}

		if (self.selectedModule() == null || self.selectedModule() == '' || self.selectedModule() == {}) {
			showError('لطفا یک ماژول برای این منبع انتخاب کنید.');
			return;
		}


		var resource = {
			moduleId: self.selectedModule().moduleId,
			resourceId: self.resourceId(),
			resourceName: self.resourceName()
		};

		if (resource.moduleId == null || resource.moduleId == '' || resource.moduleId == {}) {
			showError('لطفا یک ماژول برای این منبع انتخاب کنید.');
			return;
		}

		if (!resource.resourceName || $.trim(resource.resourceName).length == 0) {
			showError('لطفا نام ماژول را وارد کنید.');
			$('#resourceName').focus();
			return;
		}

		accountManagerApp.callApi(urls.resourcesSaveUrl, "POST", resource, function (data) {
			self.resourceCancel(); // cleanup fields.
			$('#resource-management-grid').data('kendoGrid').dataSource.read();
			$('#resource-management-grid').data('kendoGrid').refresh();
		});
	};

	self.resources = ko.observableArray();

	self.initResourceManagementGrid = function () {

		accountManagerApp.callApi(urls.appsWithModulesUrl, "GET", {}, function (data) {
			self.appList(data);

			if (data.length > 0) {
				self.selectedApp(data[0]);
			}
		});

		self.dataSource = new kendo.data.DataSource({
			transport: {
				read: {
					url: urls.resourcesUrl,
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
			pageSize: 10,
			requestEnd: onRequestEnd,
			schema: {
				model: {
					id: "id",
					fields: {
						applicationId: { editable: false, nullable: false },
						applicationName: { editable: false, nullable: false },
						moduleId: { editable: false, nullable: false },
						moduleName: { editable: false, nullable: false },
						resourceId: { editable: false, nullable: false },
						resourceName: { editable: false, nullable: false }
					}
				}
			}
		});


		$("#resource-management-grid").kendoGrid({
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
                { field: "applicationId", title: "App Id", width: 150 },
                { field: "applicationName", title: "App Name", width: 200 },
                { field: "moduleId", title: "Module Id", width: 150 },
                { field: "moduleName", title: "Module Name", width: 200 },
				{ field: "resourceId", title: "Resource Id", width: 150 },
                { field: "resourceName", title: "Resource Name", width: 200 },
                { command: { text: "Edit", click: self.editResource }, title: " ", width: "120px" },
                { command: { text: "Remove", click: self.removeResource }, title: " ", width: "120px" }
			],
			editable: false,
			dataBinding: onDataBinding
		});
	}

	self.initResourceManagementGrid();
}