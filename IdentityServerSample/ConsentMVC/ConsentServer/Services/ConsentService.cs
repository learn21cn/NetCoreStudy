using ConsentServer.ViewModels;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsentServer.Services
{
    public class ConsentService
    {
        private readonly IClientStore _clientStore;
        private readonly IResourceStore _resourceStore;
        private readonly IIdentityServerInteractionService _identityServerInteractionService;

        public ConsentService(IClientStore clientStore,IResourceStore resourceStore, IIdentityServerInteractionService identityServerInteractionService)
        {
            _clientStore = clientStore;
            _resourceStore = resourceStore;
            _identityServerInteractionService = identityServerInteractionService;
        }

        #region Private Methods
        private ConsentViewModel CreateConsentViewModel(AuthorizationRequest request,Client client, Resources resources,InputConsentViewModel model)
        {
            var rememberConsent = model?.RememberConsent ?? true;
            var selectedScopes = model?.ScopesConsented ?? Enumerable.Empty<string>();
            var vm = new ConsentViewModel();
            vm.ClientName = client.ClientName;
            vm.ClientLogoUrl = client.LogoUri;
            vm.RememberConsent = rememberConsent;

            vm.IdentityScopes = resources.IdentityResources.Select(i => CreatScopeViewModel(i,selectedScopes.Contains(i.Name)|| model==null));
            vm.ResourceScopes = resources.ApiResources.SelectMany(i => i.Scopes).Select(x=>CreatScopeViewModel(x,selectedScopes.Contains(x.Name)|| model==null));
            return vm;
        }

        private ScopeViewModel CreatScopeViewModel(IdentityResource identityResource, bool check)
        {
            return new ScopeViewModel
            {
                Name = identityResource.Name,
                DisplayName = identityResource.DisplayName,
                Description = identityResource.Description,
                Checked = check || identityResource.Required,
                Required = identityResource.Required,
                Emphasize =identityResource.Emphasize
            };
        }

        private ScopeViewModel CreatScopeViewModel(Scope scope, bool check)
        {
            return new ScopeViewModel
            {
                Name = scope.Name,
                DisplayName = scope.DisplayName,
                Description = scope.Description,
                Checked = check || scope.Required,
                Required = scope.Required,
                Emphasize = scope.Emphasize
            };
        }

        #endregion


    }
}
