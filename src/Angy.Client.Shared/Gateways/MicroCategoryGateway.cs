﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.Client.Shared.Abstract;
using Angy.Client.Shared.Adapters;
using Angy.Client.Shared.Responses;
using Angy.Model;

namespace Angy.Client.Shared.Gateways
{
    public class MicroCategoryGateway
    {
        readonly IClientAdapter _client;

        public MicroCategoryGateway(IClientAdapter client)
        {
            _client = client;
        }

        public Task<Result<IEnumerable<MicroCategory>, Error.ExceptionalError>> GetMicroCategoriesWithIdNameAndDescription()
        {
            var query = RequestAdapter<ResponsesAdapter.MicroCategoriesResponse, IEnumerable<MicroCategory>>.Build(
                "{ microcategories { id, name, description } }",
                response => response.MicroCategories!);

            return _client.SendQueryAsync(query);
        }

        public Task<Result<IEnumerable<MicroCategory>, Error.ExceptionalError>> GetMicroCategoriesWithIdAndName()
        {
            var query = RequestAdapter<ResponsesAdapter.MicroCategoriesResponse, IEnumerable<MicroCategory>>.Build("{ microcategories { id, name } }", response => response.MicroCategories);

            return _client.SendQueryAsync(query);
        }

        public Task<Result<MicroCategory, Error.ExceptionalError>> GetMicroCategoryById(Guid id)
        {
            var query = RequestAdapter<ResponsesAdapter.MicroCategoryResponse, MicroCategory>.Build(
                "query GetMicroCategoryById($id: String) { microcategory(id: $id) {id, name, description } }",
                request => request.MicroCategory!,
                new { id },
                "GetMicroCategoryById"
            );

            return _client.SendQueryAsync(query);
        }

        public Task<Result<MicroCategory, Error.ExceptionalError>> CreateMicroCategory(MicroCategory micro)
        {
            var query = RequestAdapter<ResponsesAdapter.MicroCategoryResponse, MicroCategory>.Build(
                "mutation CreateMicroCategory($microcategory: MicroCategoryInput!) { createMicroCategory(microcategory: $microcategory) { id, name, description } }",
                response => response.MicroCategory!,
                new { microcategory = SerializeMicro(micro) },
                "CreateMicroCategory"
            );

            return _client.SendQueryAsync(query);
        }

        public Task<Result<MicroCategory, Error.ExceptionalError>> UpdateMicroCategory(Guid id, MicroCategory micro)
        {
            var query = RequestAdapter<ResponsesAdapter.MicroCategoryResponse, MicroCategory>.Build(
                "mutation UpdateMicroCategory($id: String!, $microcategory: MicroCategoryInput!) { updateMicroCategory(id: $id, microcategory: $microcategory) { id, name, description } }",
                response => response.MicroCategory,
                new { microcategory = SerializeMicro(micro), id },
                "UpdateMicroCategory"
            );

            return _client.SendQueryAsync(query);
        }

        static object SerializeMicro(MicroCategory micro) => new
        {
            name = micro.Name,
            description = micro.Description
        };
    }
}