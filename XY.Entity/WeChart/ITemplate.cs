﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity
{
    /// <summary>
    /// 模板接口
    /// </summary>
    public interface ITemplate
    {
        /// <summary>
        /// 模板
        /// </summary>
        string Template { get; }
        /// <summary>
        /// 生成内容
        /// </summary>
        /// <returns>string</returns>
        string GenerateContent();
    }
}
