Overview
--------

This sample Cloud Module used in the [Integrating with Third Party Service Tutorial](https://www.parse.com/tutorials/integrating-with-third-party-services) to show developers how to create a Cloud Module. The module showcases sending email using the Mailgun service. To use the official Mailgun module, head over to the [Mailgun Cloud Module guide](https://www.parse.com/docs/cloud_modules_guide#mailgun).

Mailgun is a set of powerful APIs that allow you to send, receive, track and store email effortlessly. You can check out their service at [www.mailgun.com](http://www.mailgun.com/). To use this module, you will need to head over to the [Mailgun website](http://www.mailgun.com/) and create an account.

Installation
------------

  1. Clone this respository to get the Cloud Module.
```
git clone https://github.com/ParsePlatform/MyMailCloudModule.git
```

  2. Copy `myMailModule-1.0.0.js` over to your Cloud Code Directory, placing it in the `cloud` directory.

Usage
-----

To use the module in your Cloud Code functions, start by requiring the module and initializing it with your credentials:

```javascript
var client = require('cloud/myMailModule-1.0.0.js');
client.initialize('myDomainName', 'myAPIKey');
```

Then inside of your Cloud Code function, you can use the `sendEmail` function to fire off some emails:

```javascript
Parse.Cloud.define("sendEmailToUser", function(request, response) {
  client.sendEmail({
    to: "email@example.com",
    from: "MyMail@CloudCode.com",
    subject: "Hello from Cloud Code!",
    text: "Using Parse and My Mail Module is great!"
  }).then(function(httpResponse) {
    response.success("Email sent!");
  }, function(httpResponse) {
    console.error(httpResponse);
    response.error("Uh oh, something went wrong");
  });
});
```

This function takes two parameters. The first is a hash with the mail parameters you want to include in the request. The typical ones are from, to, subject and text, but you can find the full list on their documentation page. The second parameter to this function is an object with a success and an error field containing two callback functions.

Once you've deployed your Cloud Code function and this Cloud Module to Parse, you can test it out. The example below shows how to do this using cUrl:

```
curl -X POST \
  -H "X-Parse-Application-Id: YOUR_APPLICATION_ID" \
  -H "X-Parse-REST-API-Key: YOUR_REST_API_KEY" \
  -H "Content-Type: application/json" \
  -d '{ }' \
  https://api.parse.com/1/functions/sendEmailToUser
```

For additional information about this Cloud Module, take a look at the [API Reference](https://www.parse.com/docs/js/symbols/Mailgun.html).
