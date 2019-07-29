using System;
using System.Linq.Expressions;
using e_commerce_api.Models;
using MongoDB.Driver;

public static class ExpressionExtensions

{

    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> leftExp, Expression<Func<T, bool>> rightExp)

    {

        var sum = Expression.AndAlso(leftExp.Body, Expression.Invoke(rightExp, leftExp.Parameters[0]));

        return Expression.Lambda<Func<T, bool>>(sum, leftExp.Parameters);

    }

    public static FilterDefinition<Product> And<T>(this FilterDefinition<Product> leftFilter, FilterDefinition<Product> rightFilter)
    {
        return leftFilter & rightFilter;
    }

}