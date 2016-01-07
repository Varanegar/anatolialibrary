/**
 * Sample Mailgun Cloud Module
 * @name Mailgun
 * @namespace
 *
 * Sample Cloud Module for using <a href="http://www.mailgun.com">Mailgun</a>.
 *
 * <ul><li>Module Version: 1.0.0</li>
 * <li>Mailgun API Version: 'v2'</li></ul>
 *
 * Copyright 2013 Parse, Inc.
 * This module is freely distributable under the MIT license.
 */

(function() {

  var url = 'api.mailgun.net/v2';
  var domain = '';
  var key = '';

  module.exports = {
    /**
     * Get the version of the module.
     * @return {String}
     */
    version: '1.0.0',

    /**
     * Initialize the Mailgun module with the proper credentials.
     * @param {String} domainName Your Mailgun domain name
     * @param {String} apiKey Your Mailgun api key
     */
    initialize: function(domainName, apiKey) {
      domain = domainName;
      key = apiKey;
      return this;
    },

    /**
     * Send an email using Mailgun.
     * @param {Object} params A hash of the paramaters to be passed to
     *      the Mailgun API. They are passed as-is, so you should
     *      consult Mailgun's documentation to ensure they are valid.
     * @param {Object} options A hash with the success and error callback
     *      functions under the keys 'success' and 'error' respectively.
     * @return {Parse.Promise}
     */
    sendEmail: function(params, options) {
      return Parse.Cloud.httpRequest({
        method: "POST",
        url: "https://api:" + key + "@" + url + "/" + domain + "/messages",
        body: params,
      }).then(function(httpResponse) {
        if (options && options.success) {
          options.success(httpResponse);
        }
      }, function(httpResponse) {
        if (options && options.error) {
          options.error(httpResponse);
        }
      });
    }

  }
}());
