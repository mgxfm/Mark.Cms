﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Sample05
{
    /// <summary>
    /// 内容实体
    /// </summary>
    public class ContentWithComment
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime add_time { get; set; } = DateTime.Now;
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? modify_time { get; set; }
        /// <summary>
        /// 评论
        /// </summary>
        public IEnumerable<Comment> comments { get; set; }
    }
}
