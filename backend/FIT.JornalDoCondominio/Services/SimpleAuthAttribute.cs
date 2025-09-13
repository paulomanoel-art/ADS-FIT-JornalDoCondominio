using FIT.JornalDoCondominio.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FIT.JornalDoCondominio.Services
{
    public class SimpleAuthAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var db = context.HttpContext.RequestServices.GetService<JornalDoCondominioDbContext>();
            var email = context.HttpContext.Request.Headers["X-User-Email"].ToString();
            var senha = context.HttpContext.Request.Headers["X-User-Password"].ToString();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha) ||
                !db.Usuarios.Any(u => u.EmailLogin == email && u.PwdLogin == senha && u.Ativo))
            {
                context.Result = new UnauthorizedResult();
            }

            base.OnActionExecuting(context);
        }
    }
}
