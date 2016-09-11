using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Msr.Services.jqGrid
{
    public static class ExtendedMethods
    {
        #region '----- Method(s) -----'
        /// <summary>
        /// Convertors for null date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ConvertorForNullDateTime(this DateTime? value)
        {
            if (value.HasValue)
                return value.Value.ToShortDateString();
            else
                return string.Empty;
        }

        /// <summary>
        /// Convertors for date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ConvertorForDateTime(this DateTime value)
        {
            if (value == null || value == DateTime.MinValue)
                return string.Empty;
            else
                return value.ToShortDateString();
        }

        /// <summary>
        /// Orders the by.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "OrderBy");
        }

        /// <summary>
        /// Orders the by descending.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "OrderByDescending");
        }

        /// <summary>
        /// Thens the by.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "ThenBy");
        }

        /// <summary>
        /// Thens the by descending.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "ThenByDescending");
        }

        /// <summary>
        /// Applies the order.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="property">The property.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <returns></returns>
        static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {

                PropertyInfo pi = type.GetProperty(prop);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                method => method.Name == methodName
                          && method.IsGenericMethodDefinition
                          && method.GetGenericArguments().Length == 2
                          && method.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), type)
                .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }

        /// <summary>
        /// To set the currency format in " $X,XXX.XX " format. Data type should be float.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string CurrencyFormat(this float value)
        {
            return string.Format("{0:C}", value);
        }

        /// <summary>
        /// To set the currency format in " $X,XXX.XX " format of variable number of decimal places. Data type should be float.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="numberOfDecimalPlaces"></param>
        /// <returns></returns>
        public static string CurrencyFormat(this float value, int numberOfDecimalPlaces)
        {
            string format = "{0:C" + numberOfDecimalPlaces.ToString()+"}";
            return string.Format(format, value);
        }

        /// <summary>
        /// To set the currency format in " $X,XXX.XX " format. Data type should be decimal.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string CurrencyFormat(this decimal value)
        {
            return string.Format("{0:C}", value);
        }


        /// <summary>
        /// To set the currency format in " $X,XXX " format. Data type should be decimal.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string CurrencyFormat(this Int32 value)
        {
            return string.Format("{0:C0}", value);
        }

        /// <summary>
        /// To set the currency format in " $X,XXX " format. Data type should be decimal.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string NumberFormat(this Int32 value)
        {
            return string.Format("{0:N0}", value);
        }

        /// <summary>
        /// To set the number format in " X,XXX " format without currency symbol. Data type should be decimal.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string NumberFormat(this decimal value)
        {
            return string.Format("{0:N2}", value);
        }

        /// <summary>
        /// To set the currency format in " $X,XXX.XX " format of variable number of decimal places. Data type should be decimal.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="numberOfDecimalPlaces"></param>
        /// <returns></returns>
        public static string CurrencyFormat(this decimal value, int numberOfDecimalPlaces)
        {
            string format = "{0:C" + numberOfDecimalPlaces.ToString() + "}";
            return string.Format(format, value);
        }

        public static string CurrencyFormat(this decimal? value, int numberOfDecimalPlaces)
        {
            if (!value.HasValue)
            {
                return "";
            }
            string format = "{0:C" + numberOfDecimalPlaces.ToString() + "}";
            return string.Format(format, value);
        }

        /// <summary>
        /// To set the currency format in " $X,XXX.XX " format. Data type should be double.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string CurrencyFormat(this double value)
        {
            return string.Format("{0:C}", value);
        }

        /// <summary>
        /// To set the currency format in " $X,XXX.XX " format of variable number of decimal places. Data type should be double.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="numberOfDecimalPlaces"></param>
        /// <returns></returns>
        public static string CurrencyFormat(this double value, int numberOfDecimalPlaces)
        {
            string format = "{0:C" + numberOfDecimalPlaces.ToString() + "}";
            return string.Format(format, value);
        }

        /// <summary>
        /// To set the Gallons format in " $X,XXX " format. Data type should be decimal.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GallonsFormat(this decimal? value)
        {
            decimal formattedValue = value.HasValue?  (decimal)value:0m;
            return formattedValue.ToString("#,##0.00");
        }
        /// <summary>
        /// Wheres the in.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="selector">The selector.</param>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        public static IQueryable<TEntity> WhereIn<TEntity, TValue>
            (
            this ObjectQuery<TEntity> query,
            Expression<Func<TEntity, TValue>> selector,
            IEnumerable<TValue> collection
            )
        {
            ParameterExpression p = selector.Parameters.Single();

            //if there are no elements to the WHERE clause,
            //we want no matches:
            if (!collection.Any()) return query.Where(x => false);

            if (collection.Count() > 3000) //could move this value to config
                throw new ArgumentException("Collection too large - execution will cause stack overflow", "collection");

            IEnumerable<Expression> equals = collection.Select(value =>
                (Expression)Expression.Equal(selector.Body,
                    Expression.Constant(value, typeof(TValue))));

            Expression body = equals.Aggregate((accumulate, equal) =>
                Expression.Or(accumulate, equal));

            return query.Where(Expression.Lambda<Func<TEntity, bool>>(body, p));
        }

        #endregion
    }
}
