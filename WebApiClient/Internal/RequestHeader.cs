﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace WebApiClient
{
    /// <summary>
    /// 提供请求头枚举到名称的转换
    /// </summary>
    static class RequestHeader
    {
        /// <summary>
        /// HttpRequestHeader的类型
        /// </summary>
        private static readonly Type httpRequestHeaderType = typeof(HttpRequestHeader);

        /// <summary>
        /// 请求头枚举和名称的缓存
        /// </summary>
        private static readonly Dictionary<HttpRequestHeader, string> cache = new Dictionary<HttpRequestHeader, string>();

        /// <summary>
        /// 请求头枚举到名称的转换
        /// </summary>
        static RequestHeader()
        {
            var enums = Enum.GetValues(httpRequestHeaderType).Cast<HttpRequestHeader>();
            foreach (var item in enums)
            {
                cache.Add(item, item.GetDisplayName());
            }
        }

        /// <summary>
        /// 返回枚举的DisplayName
        /// </summary>
        /// <param name="header">请求头枚举</param>
        /// <returns></returns>
        private static string GetDisplayName(this HttpRequestHeader header)
        {
            return httpRequestHeaderType
                .GetField(header.ToString())
                .GetCustomAttribute<DisplayAttribute>()
                .Name;
        }

        /// <summary>
        /// 获取请求头名称
        /// </summary>
        /// <param name="header">请求头枚举</param>
        /// <returns></returns>
        public static string GetName(HttpRequestHeader header)
        {
            cache.TryGetValue(header, out string name);
            return name;
        }
    }
}