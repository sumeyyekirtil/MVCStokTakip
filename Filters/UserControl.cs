using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters; //filtre kullanabilmek için gerekli kütüphane miras alınarak import edildi

namespace MVCStokTakip.Filters
{
	public class UserControl : ActionFilterAttribute
	{
		public override void OnActionExecuted(ActionExecutedContext context)
		{
			base.OnActionExecuted(context);
		}

		public override void OnActionExecuting(ActionExecutingContext context)
		{
			base.OnActionExecuting(context);
		}

		public override void OnResultExecuting(ResultExecutingContext context)
		{
			base.OnResultExecuting(context);
		}

		public override void OnResultExecuted(ResultExecutedContext context)
		{
			base.OnResultExecuted(context);
		}
	}
}
