Parse.Cloud.define("getInstallationUserList", function (request, response) {

    var query = new Parse.Query(Parse.Installation);
    //query.equalTo('injuryReports', true);
    query.find({ useMasterKey: true,
        error: function(error) {
            response.error('Failed to create new object, with error code: ' + error.message);
        },
        success: function(object) {
            response.success(object);
        }
    }); 
});


Parse.Cloud.define("sendPush", function (request, response) {

    var query = new Parse.Query(Parse.Installation);
    //query.equalTo('injuryReports', true);
    query.find({
        useMasterKey: true,
        error: function (error) {
            response.error('Failed to create new object, with error code: ' + error.message);
        },
        success: function (object) {
            response.success(object);
        }
    });
});
