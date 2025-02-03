using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace GestaoContas.Api.Extensions.Authorizations
{
    public class RequisitionClaimFilter : IAuthorizationFilter
    {
        private readonly Claim _claim;

        public RequisitionClaimFilter(Claim claim)
        {
            _claim = claim;
        }


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity!.IsAuthenticated)
            {
                context.Result = new StatusCodeResult(401);
                return;
            }

            if (!CustomAuthorization.ValidarClaimUsuario(context.HttpContext, _claim.Type, _claim.Value))
                context.Result = new StatusCodeResult(403);
        }
    }
}
