using System;
using GraphQL;

namespace Angy.Client.Shared.Adapters
{
    public class RequestAdapter<TResponse, TCast>
    {
        public GraphQLRequest Request { get; }
        public Func<TResponse, TCast> Selector { get; }

        protected RequestAdapter(string query, Func<TResponse, TCast> selector, object? variables = default, string? name = default)
        {
            Request = new GraphQLRequest
            {
                Query = query,
                Variables = variables,
                OperationName = name
            };

            Selector = selector;
        }

        public static RequestAdapter<TResponse, TCast> Build(string query, Func<TResponse, TCast> selector, object? variables = default, string? name = default) =>
            new RequestAdapter<TResponse, TCast>(query, selector, variables, name);
    }

    public sealed class RequestAdapter<TResponse> : RequestAdapter<TResponse, TResponse>
    {
        RequestAdapter(string query, object? variables = default, string? name = default) : base(query, r => r, variables, name)
        {
        }

        public static RequestAdapter<TResponse> Build(string query, object? variables = default, string? name = default) =>
            new RequestAdapter<TResponse>(query, variables, name);
    }
}