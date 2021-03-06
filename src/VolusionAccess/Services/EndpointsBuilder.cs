﻿using System.Globalization;
using VolusionAccess.Models.Command;
using VolusionAccess.Models.Configuration;

namespace VolusionAccess.Services
{
	internal static class EndpointsBuilder
	{
		public static readonly string EmptyParams = string.Empty;

		public static string CreateGetPublicProductsEndpoint()
		{
			var endpoint = string.Format( "{0}={1}", VolusionParam.ApiName.Name, VolusionCommand.GetPublicProducts.Command );
			return endpoint;
		}

		public static string CreateGetProductsEndpoint()
		{
			var endpoint = string.Format( "{0}={1}&{2}={3}",
				VolusionParam.ApiName.Name, VolusionCommand.GetProducts.Command,
				VolusionParam.SelectColumns.Name, GetProductColumns() );
			return endpoint;
		}

		public static string CreateGetFilteredProductsEndpoint( ProductColumns column, object value )
		{
			var endpoint = string.Format( _culture, "{0}={1}&{2}={3}&{4}={5}&{6}={7}",
				VolusionParam.ApiName.Name, VolusionCommand.GetProducts.Command,
				VolusionParam.SelectColumns.Name, GetProductColumns(),
				VolusionParam.WhereColumn.Name, column.Name,
				VolusionParam.WhereValue.Name, value );
			return endpoint;
		}

		public static string CreateGetProductEndpoint( string sku )
		{
			return CreateGetFilteredProductsEndpoint( ProductColumns.Sku, sku );
		}

		public static string CreateGetChildProductsEndpoint( string sku )
		{
			return CreateGetFilteredProductsEndpoint( ProductColumns.IsChildOfSku, sku );
		}

		public static string CreateProductsUpdateEndpoint()
		{
			var endpoint = string.Format( "{0}={1}", VolusionParam.Import.Name, VolusionParam.Update.Name );
			return endpoint;
		}

		public static string CreateGetOrdersEndpoint()
		{
			var endpoint = string.Format( "{0}={1}&{2}={3}",
				VolusionParam.ApiName.Name, VolusionCommand.GetOrders.Command,
				VolusionParam.SelectColumns.Name, "*" );
			return endpoint;
		}

		public static string CreateGetFilteredOrdersEndpoint( OrderColumns column, object value )
		{
			var endpoint = string.Format( _culture, "{0}={1}&{2}={3}&{4}={5}&{6}={7}",
				VolusionParam.ApiName.Name, VolusionCommand.GetOrders.Command,
				VolusionParam.SelectColumns.Name, "*",
				VolusionParam.WhereColumn.Name, column.Name,
				VolusionParam.WhereValue.Name, value );
			return endpoint;
		}

		public static string GetFullEndpoint( this string endpoint, VolusionConfig config )
		{
			var fullEndpoint = string.Format( "{0}?{1}", config.Host, endpoint );
			return fullEndpoint;
		}

		public static string GetFullEndpointWithAuth( this string endpoint, VolusionConfig config )
		{
			var fullEndpoint = string.Format( "{0}?{1}={2}&{3}={4}&{5}",
				config.Host,
				VolusionParam.Login.Name, config.UserName,
				VolusionParam.EncryptedPassword.Name, config.Password,
				endpoint );
			return fullEndpoint;
		}

		private static string GetProductColumns()
		{
			var columns = string.Format( "{0},{1},{2},{3},{4},{5},{6},{7},{8}",
				ProductColumns.ProductID.Name,
				ProductColumns.Sku.Name,
				ProductColumns.Quantity.Name,
				ProductColumns.ProductName.Name,
				ProductColumns.LastModified.Name,
				ProductColumns.ProductPrice.Name,
				ProductColumns.SalePrice.Name,
				ProductColumns.IsChildOfSku.Name,
				ProductColumns.Warehouses.Name );
			return columns;
		}

		private static readonly CultureInfo _culture = new CultureInfo( "en-US" );
	}
}