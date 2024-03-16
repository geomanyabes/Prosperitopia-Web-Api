using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Prosperitopia.Domain.Interface;
using Prosperitopia.Domain.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Prosperitopia.Application.Extension
{
    public static class QueryableExtension
    {
        public static IQueryable<TSource> ApplySearch<TSource>(
            this IQueryable<TSource> queryable,
            string search,
            string propertyName, 
            SearchType searchType)
        {
            if (string.IsNullOrWhiteSpace(search))
                return queryable;

            var parameter = Expression.Parameter(typeof(TSource), "x");
            var property = Expression.Property(parameter, propertyName);
            var propertyType = typeof(TSource).GetProperty(propertyName)?.PropertyType;

            if (propertyType == null)
                throw new ArgumentException($"Property '{propertyName}' not found on type '{typeof(TSource)}'");

            var searchExpression = GetSearchExpression(property, search, searchType, propertyType);
            var lambda = Expression.Lambda<Func<TSource, bool>>(searchExpression, parameter);

            return queryable.Where(lambda);
        }

        private static Expression GetSearchExpression(MemberExpression property, string search, SearchType searchType, Type propertyType, bool ignoreCase = false)
        {
            var searchValue = Expression.Constant(search);
            Expression expression;

            switch (searchType)
            {
                case SearchType.EXACT:
                    expression = Expression.Equal(property, searchValue);
                    break;
                case SearchType.CONTAINS:
                    expression = Expression.Call(property, "Contains", null, searchValue);
                    break;
                case SearchType.ENDS_WITH:
                    expression = Expression.Call(property, "EndsWith", null, searchValue);
                    break;
                case SearchType.STARTS_WITH:
                    expression = Expression.Call(property, "StartsWith", null, searchValue);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(searchType), "Invalid search type");
            }

            if (ignoreCase)
                expression = Expression.Call(typeof(string), "ToLower", null, expression);

            return expression;
        }

        public static IOrderedQueryable<TSource> OrderByString<TSource>(this IQueryable<TSource> query, string propertyName, string direction)
        {
            var entityType = typeof(TSource);

            //Create x=>x.PropName
            var propertyInfo = entityType.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            ParameterExpression arg = Expression.Parameter(entityType, "x");
            MemberExpression property = Expression.Property(arg, propertyName);
            var selector = Expression.Lambda(property, new ParameterExpression[] { arg });

            //Get System.Linq.Queryable.OrderBy() || OrderByDescending method.
            string methodName = direction.ToLower() == "asc" ? "OrderBy" : "OrderByDescending";
            var enumarableType = typeof(System.Linq.Queryable);
            var method = enumarableType.GetMethods()
                 .Where(m => m.Name == methodName && m.IsGenericMethodDefinition)
                 .Where(m =>
                 {
                     var parameters = m.GetParameters().ToList();
                     //Put more restriction here to ensure selecting the right overload                
                     return parameters.Count == 2;//overload that has 2 parameters
                 }).Single();
            //The linq's OrderBy<TSource, TKey> has two generic types, which provided here
            MethodInfo genericMethod = method
                 .MakeGenericMethod(entityType, propertyInfo.PropertyType);

            /*Call query.OrderBy(selector), with query and selector: x=> x.PropName
              Note that we pass the selector as Expression to the method and we don't compile it.
              By doing so EF can extract "order by" columns and generate SQL for it.*/
            var newQuery = (IOrderedQueryable<TSource>)genericMethod
                 .Invoke(genericMethod, new object[] { query, selector });
            return newQuery;
        }

    }
}
