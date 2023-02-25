namespace Zero.AspNetCore.Mvc.Route
{
    public static class MvcOptionsExtensions
    {
        /// <summary>
        /// 添加路由前缀
        /// </summary>
        /// <param name="opts"></param>
        /// <param name="routePrefix"></param>
        public static void AddRoutePrefix(this MvcOptions opts, string routePrefix)
        {
            opts.Conventions.Insert(0, new RoutePrefixConvention(new RouteAttribute(routePrefix)));
        }
    }
}
