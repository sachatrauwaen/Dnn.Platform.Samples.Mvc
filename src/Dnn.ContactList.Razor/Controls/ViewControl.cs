﻿using Dnn.ContactList.Api;
using Dnn.ContactList.Razor.Models;
using DotNetNuke.Collections;
using DotNetNuke.Common;
using DotNetNuke.Web.MvcPipeline.ModuleControl;
using DotNetNuke.Web.MvcPipeline.ModuleControl.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dnn.ContactList.Razor
{

    public class ViewControl : RazorModuleControlBase
    {
        private readonly IContactRepository _repository;

        public ViewControl() : this(ContactRepository.Instance)
        {

        }
        public ViewControl(IContactRepository repository)
        {
            Requires.NotNull(repository);

            _repository = repository;
            LocalResourceFile = "~/DesktopModules/Dnn/RazorContactList/App_LocalResources/Contact.resx";
        }

        public override string ControlName => "View";



        public override IRazorModuleResult Invoke()
        {
            var contacts = _repository.GetContacts(PortalSettings.PortalId);

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