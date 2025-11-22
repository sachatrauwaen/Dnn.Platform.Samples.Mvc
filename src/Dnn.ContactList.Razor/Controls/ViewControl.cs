using Dnn.ContactList.Api;
using Dnn.ContactList.Razor.Models;
using DotNetNuke.Abstractions.Pages;
using DotNetNuke.Collections;
using DotNetNuke.Common;
using DotNetNuke.Services.Pages;
using DotNetNuke.Web.MvcPipeline.ModuleControl;
using DotNetNuke.Web.MvcPipeline.ModuleControl.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;

namespace Dnn.ContactList.Razor
{

    public class ViewControl : RazorModuleControlBase
    {
        private readonly IContactRepository _repository;
        private readonly IPageService pageService;

        /*
        public ViewControl() : this(ContactRepository.Instance)
        {

        }
        */

        public ViewControl(IPageService pageService)
        {
            //Requires.NotNull(repository);
            this.pageService = pageService;
            _repository = ContactRepository.Instance;
            LocalResourceFile = "~/DesktopModules/Dnn/RazorContactList/App_LocalResources/Contact.resx";
            this.pageService = pageService;
        }

        public override string ControlName => "View";

        public override IRazorModuleResult Invoke()
        {
            var contacts = _repository.GetContacts(PortalSettings.PortalId);
            
            this.pageService.SetTitle("Contact List - " +contacts.Count());
            this.pageService.SetDescription("Contact List description - " + contacts.Count());
            this.pageService.SetKeyWords("keywords1");


            //this.pageService.AddInfoMessage("","This is a simple contact list module built using Razor and DNN's MVC Pipeline");
            //this.pageService.AddErrorMessage("", "This is a simple contact list module built using Razor and DNN's MVC Pipeline");

            return View(new ContactsModel()
            {
                IsEditable = ModuleContext.IsEditable,
                EditUrl = ModuleContext.EditUrl(),
                Contacts = contacts.Select(c => new ContactModel()
                {
                    ContactId = c.ContactId,
                    Email = c.Email,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Phone = c.Phone,
                    Twitter = c.Twitter,
                    IsEditable = ModuleContext.IsEditable,
                    EditUrl = ModuleContext.IsEditable ? this.EditUrl("ContactId", c.ContactId.ToString(), "Edit") : string.Empty
                }).ToList()
            });
        }
    }
}