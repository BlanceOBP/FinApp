using FinApp.Core.Enums;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System.Linq.Expressions;

namespace FinApp.Core.Extensions
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// Сортировка по напралению и свойству
        /// </summary>
        /// <param name="source">Исходный набор элементов в виде <see cref="IQueryable{T}"/></param>
        /// <param name="order">Порядок сортировки</param>
        /// <param name="propertyName">Свойство для сортировки</param>
        /// <typeparam name="TSource">Тип элемента из набора</typeparam>
        /// <returns>Отсортированный набор элементов в виде <see cref="IOrderedQueryable{T}"/></returns>
        public static IOrderedQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> source, SortingDirection? order, string propertyName)
        {
            return order == SortingDirection.Desc
                ? source.CallOrderedQueryable("OrderByDescending", propertyName)
                : source.CallOrderedQueryable("OrderBy", propertyName);
        }

        /// <summary>
        /// Генерация вызова методов Queryable с использованием наименования свойства
        /// </summary>
        private static IOrderedQueryable<T> CallOrderedQueryable<T>(this IQueryable<T> query, string methodName,
            string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                propertyName = typeof(T).GetProperties().First().Name;

            var param = Expression.Parameter(typeof(T), "a");

            var body = propertyName.Split('.').Aggregate<string, Expression>(param, Expression.PropertyOrField);

            return (IOrderedQueryable<T>)query.Provider.CreateQuery(
                Expression.Call(
                    typeof(Queryable),
                    methodName,
                    new[] { typeof(T), body.Type },
                    query.Expression,
                    Expression.Lambda(body, param)
                )
            );
        }
    }
}
