﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace GBNewEnergy.Protocol.Extensions
{
    /// <summary>
    /// 枚举扩展
    /// </summary>
    internal static class EnumExtensions
    {
        /// <summary>
        /// 转为整型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static int ToValue<T>(this T t) where T : struct
        {
            return Convert.ToInt32(t);
        }
        /// <summary>
        /// 转为Byte
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static byte ToByteValue<T>(this T t) where T : struct
        {
            return Convert.ToByte(t);
        }

        /// <summary>
        /// 字符转枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value) where T : struct
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        /// <summary>
        /// 获取枚举字符串
        /// </summary>
        /// <param name="valueEnum"></param>
        public static string GetName(this Enum valueEnum)
        {
            return valueEnum.ToString();
        }

        /// <summary>
        /// 获取DescriptionAttribute特性枚举值的描述
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            var attribute = value.GetAttribute<DescriptionAttribute>();
            return attribute == null ? value.ToString() : attribute.Description;
        }

        /// <summary>
        /// 验证是否是枚举类型
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static bool IsEnumValid<TEnum>(this int enumValue)
        {
            return Enum.IsDefined(typeof(TEnum), enumValue);
        }

        /// <summary>
        /// 获取DescriptionAttribute特性枚举及描述
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDescriptionAttributeDictionary(this Enum value)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            var fields = value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (var fi in fields)
            {
                DescriptionAttribute attr = Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute), false) as DescriptionAttribute;
                dictionary.Add(fi.Name, attr != null ? attr.Description : "");
            }
            return dictionary;
        }

        /// <summary>
        /// 获取DisplayNameAttribute特性枚举值的描述
        /// </summary>
        /// <param name="obj">枚举值</param>
        /// <returns></returns>
        public static string GetDisplayName(this Enum value)
        {
            var attribute = value.GetAttribute<DisplayNameAttribute>();
            return attribute == null ? value.ToString() : attribute.DisplayName;
        }

        /// <summary>
        /// 获取DisplayNameAttribute特性枚举及描述
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDisplayNameAttributeDictionary(this Enum value)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            var fields = value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (var fi in fields)
            {
                DisplayNameAttribute attr = Attribute.GetCustomAttribute(fi, typeof(DisplayNameAttribute), false) as DisplayNameAttribute;
                dictionary.Add(fi.Name, attr != null ? attr.DisplayName : "");
            }
            return dictionary;
        }

        /// <summary>
        /// 获取枚举对应特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
            return (T)attributes[0];
        }
    }
}