Crowd SSO C#
=========

Implementation of authentication, passwords, details for the Atlassian Crowd Single Sign On service.

----

Includes
----

  - Authentication
  - Change Password
  - Update Password
  - Retrieve User Detail
  - Retrieve User Attributes
  - Retrieve Users In Group
 
----

Requires
----

[Json.NET]  

[Json.NET]:https://www.nuget.org/packages/newtonsoft.json/

----

Example
----

This is an example of authenicating a user from a form (MVC) if the u


  

        CrowdSSO sso = new CrowdSSO("https://server/crowd/", "example_app_name", "z2Ndj8RxMQik%Ruf^Hs0!WO7j#");
        
        [HttpPost, ValidateInput(false)]
        public ActionResult Login(UserLogin model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    HttpStatusCode authorised = sso.Authenticate(model.username, model.password);

                    switch (authorised)
                    {
                        case HttpStatusCode.OK:
                            UserDetail UserDetail = new UserDetail();

                            UserDetail = sso.UserDetail(model.username);
                            
                            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                                  1,                                     // ticket version
                                  model.username,                        // authenticated username
                                  DateTime.Now,                          // issueDate
                                  DateTime.Now.AddDays(30),              // expiryDate
                                  true,                                  // true to persist across browser sessions
                                  UserDetail.ToString(),                 // serialise a UserDetail object
                                  FormsAuthentication.FormsCookiePath    // the path for the cookie
                            );

                            // Encrypt the ticket using the machine key
                            string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                            // Add the cookie to the request to save it
                            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                            cookie.HttpOnly = true;
                            Response.Cookies.Add(cookie);


                            //Send them on their way
                            return RedirectToAction("Index", "Overview", new { area = "Dashboard" });

                        case HttpStatusCode.NotFound:
                            ModelState.AddModelError("", "Your username or password is incorrect...");
                            return View(model);

                        default:
                            //If it's not OK or a Bad Request then something went wrong
                            ModelState.AddModelError("", "There is currently an issue connecting to the server");
                            return View(model);
                    }             
                }   
                else
                {
                    return View(model);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }


        } 



License
----

MIT
