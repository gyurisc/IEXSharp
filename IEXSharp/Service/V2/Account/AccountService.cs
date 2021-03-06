﻿using VSLee.IEXSharp.Helper;
using VSLee.IEXSharp.Model.Account.Request;
using VSLee.IEXSharp.Model.Account.Response;
using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;

namespace VSLee.IEXSharp.Service.V2.Account
{
	internal class AccountService : IAccountService
	{
		private readonly string _sk;
		private readonly ExecutorREST _executor;

		public AccountService(HttpClient client, string sk, string pk, bool sign)
		{
			_sk = sk;
			_executor = new ExecutorREST(client, sk, pk, sign);
		}

		public async Task<MetadataResponse> MetadataAsync()
		{
			const string urlPattern = "account/metadata";

			var qsb = new QueryStringBuilder();
			qsb.Add("token", _sk);

			var pathNVC = new NameValueCollection();

			return await _executor.ExecuteAsyncLegacy<MetadataResponse>(urlPattern, pathNVC, qsb);
		}

		public async Task<UsageResponse> UsageAsync(UsageType type)
		{
			const string urlPattern = "account/usage/[type]";

			var qsb = new QueryStringBuilder();
			qsb.Add("token", _sk);

			var pathNVC = new NameValueCollection { { "type", type.ToString().ToLower() } };

			return await _executor.ExecuteAsyncLegacy<UsageResponse>(urlPattern, pathNVC, qsb);
		}

		public Task PayAsYouGoAsync(bool allow)
		{
			throw new NotImplementedException("Not implemented due to API failed");
		}
	}
}