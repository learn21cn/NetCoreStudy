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
            var vm = new ConsentViewModel
            {
                ClientName = client.ClientName,
                ClientLogoUrl = client.LogoUri,
                RememberConsent = rememberConsent,

                IdentityScopes = resources.IdentityResources.Select(i => CreateScopeViewModel(i, selectedScopes.Contains(i.Name) || model == null)),
                ResourceScopes = resources.ApiResources.SelectMany(i => i.Scopes).Select(x => CreateScopeViewModel(x, selectedScopes.Contains(x.Name) || model == null))
            };           
            return vm;
        }

        private ScopeViewModel CreateScopeViewModel(IdentityResource identityResource, bool check)
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

        private ScopeViewModel CreateScopeViewModel(Scope scope, bool check)
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

        public async Task<ConsentViewModel> BuildConsentViewModel(string returnUrl, InputConsentViewModel model=null)
        {
            var request = await _identityServerInteractionService.GetAuthorizationContextAsync(returnUrl);
            if (request == null)
            {
                return null;
            }
            var client = await _clientStore.FindEnabledClientByIdAsync(request.ClientId);
            var resources = await _resourceStore.FindEnabledResourcesByScopeAsync(request.ScopesRequested);

            var vm = CreateConsentViewModel(request,client,resources,model);
            vm.ReturnUrl = returnUrl;
            return vm;

        }

        public async Task<ProcessConsentResult> ProcessConsent(InputConsentViewModel viewModel)
        {
            ConsentResponse consentResponse = null;
            var result = new ProcessConsentResult();
            if (viewModel.Button == "no")
            {
                consentResponse = ConsentResponse.Denied;
            }
            else if (viewModel.Button == "yes")
            {
                if (viewModel.ScopesConsented != null && viewModel.ScopesConsented.Any())
                {
                    consentResponse = new ConsentResponse
                    {
                        RememberConsent = viewModel.RememberConsent,
                        ScopesConsented=viewModel.ScopesConsented
                    };
                }

                result.ValidationError = "至少选中一个权限";
            }

            if (consentResponse != null)
            {
                var request = await _identityServerInteractionService.GetAuthorizationContextAsync(viewModel.ReturnUrl);
                await _identityServerInteractionService.GrantConsentAsync(request, consentResponse);

                result.RedirectUrl = viewModel.ReturnUrl;
            }

            {
                var consentViewModel = await BuildConsentViewModel(viewModel.ReturnUrl, viewModel);
                result.ViewModel = consentViewModel;

            }
            return result;

        }
    }
}
