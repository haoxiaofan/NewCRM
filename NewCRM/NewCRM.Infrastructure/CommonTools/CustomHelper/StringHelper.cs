﻿using System;
using System.Linq;
using Newtonsoft.Json;

namespace NewCRM.Infrastructure.CommonTools.CustomHelper
{
    /// <summary>
    /// 字符串
    /// </summary>
    public sealed class StringHelper
    {
        public static Int32[] GetIntArrayFromString(String commaSeparatedstring)
        {
            return string.IsNullOrEmpty(commaSeparatedstring) ? new Int32[0] : commaSeparatedstring.Split(',').Select(s => Convert.ToInt32(s)).ToArray();
        }

        /// <summary>
        ///     把对象转换成Json字符串表示形式
        /// </summary>
        /// <param name="jsonObject"></param>
        /// <returns></returns>
        public static string BuildJsonString(object jsonObject)
        {
            PublicHelper.VaildateArgument(jsonObject, "jsonObject");
            return JsonConvert.SerializeObject(jsonObject);
        }

        /// <summary>
        ///     判断指定字符串是否对象（Object）类型的Json字符串格式
        /// </summary>
        /// <param name="input">要判断的Json字符串</param>
        /// <returns></returns>
        public static bool IsJsonObjectString(string input)
        {
            return input != null && input.StartsWith("{") && input.EndsWith("}");
        }

        /// <summary>
        ///     判断指定字符串是否集合类型的Json字符串格式
        /// </summary>
        /// <param name="input">要判断的Json字符串</param>
        /// <returns></returns>
        public static bool IsJsonArrayString(string input)
        {
            return input != null && input.StartsWith("[") && input.EndsWith("]");
        }
    }
}
