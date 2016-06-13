var urls = {
	loginUrl: baseBackendUrl + '/oauth/token',
	storesUrl: baseBackendUrl + '/api/gateway/stock/stocks/',
	reorderCalcTypeUrl: baseBackendUrl + "/api/gateway/basedata/reordercalctypes",

	usersUrl: baseBackendUrl + "/api/accounts/users",
	userUrl: baseBackendUrl + "/api/accounts/getUser",
	saveUserUrl: baseBackendUrl + '/api/accounts/saveUser',
	checkUserEmail: baseBackendUrl + '/api/accounts/checkEmailExist',

	permissionCatalogsUrl: baseBackendUrl + "/api/accounts/permissionCatalogs",
	permissionCatalogsOfUserUrl: baseBackendUrl + "/api/accounts/getPersmissionCatalogsOfUser",
	savePermissionCatalogsUrl: baseBackendUrl + "/api/accounts/savePermissionCatalogs",

	allPermissionsUrl: baseBackendUrl + "/api/accounts/allPermissions",
	allPermissionsOfCatalogUrl: baseBackendUrl + "/api/accounts/allPermissionsOfCatalog",
	permissionsUrl: baseBackendUrl + "/api/accounts/permissions",
	permissionsOfCatalogUrl: baseBackendUrl + '/api/accounts/permissionsOfCatalog',
	savePermissionsUrl: baseBackendUrl + "/api/accounts/savePermissions",
	permissionsOfUserUrl: baseBackendUrl + "/api/accounts/getPersmissionsOfUser",

	permissionActionsUrl: baseBackendUrl + "/api/action/list",
	permissionsSaveUrl: baseBackendUrl + '/api/permissions/save',
	permissionRemoveUrl: baseBackendUrl + '/api/permissions/remove',
	permissionToCatalogUrl: baseBackendUrl + '/api/permissions/addToCatalog',
	permissionRemoveFromCatalogUrl: baseBackendUrl + '/api/permissions/removeFromCatalog',

	SuppliersUrl: baseBackendUrl + '/api/gateway/base/supplier/suppliers',
	filterSuppliersUrl: baseBackendUrl + '/api/gateway/base/supplier/filterSuppliers',

	myWebpages: baseBackendUrl + '/api/accounts/myWebpages',

	sendPassCodeUrl: baseBackendUrl + '/api/accounts/SendPassCode',
	resetPasswordByCodeUrl: baseBackendUrl + '/api/accounts/ResetPasswordByCode',
	changePasswordUrl: baseBackendUrl + '/api/accounts/ChangePassword',

    /* apps urls */
	appsUrl: baseBackendUrl + '/api/apps/list',
	appsSaveUrl: baseBackendUrl + '/api/apps/save',
	appsRemoveUrl: baseBackendUrl + '/api/apps/remove',
	appsWithModulesUrl: baseBackendUrl + '/api/apps/appsWithModules',

    /* modules urls */
	modulesUrl: baseBackendUrl + '/api/modules/list',
	modulesSaveUrl: baseBackendUrl + '/api/modules/save',
	modulesRemoveUrl: baseBackendUrl + '/api/modules/remove',

    /* resources urls */
	resourcesUrl: baseBackendUrl + '/api/resources/list',
	resourcesSaveUrl: baseBackendUrl + '/api/resources/save',
	resourcesRemoveUrl: baseBackendUrl + '/api/resources/remove',

	pages: {
		usermanagement: { url: '/UserManager', title: 'مدیریت کاربران', order: 1 },
		permission: { url: '/UserManager/permissions', title: 'مجوز دسترسی', order: 2 },
	}
};