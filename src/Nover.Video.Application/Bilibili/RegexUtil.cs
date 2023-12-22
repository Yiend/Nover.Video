using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Nover.Video.Application
{
    public static partial class RegexUtil
    {
        [GeneratedRegex("av(\\d+)")]
        public static partial Regex AvRegex();
        [GeneratedRegex("[Bb][Vv]1(\\w+)")]
        public static partial Regex BVRegex();
        [GeneratedRegex("/ep(\\d+)")]
        public static partial Regex EpRegex();
        [GeneratedRegex("/ss(\\d+)")]
        public static partial Regex SsRegex();
        [GeneratedRegex("space\\.bilibili\\.com/(\\d+)")]
        public static partial Regex UidRegex();
        [GeneratedRegex("global\\.bilibili\\.com/play/\\d+/(\\d+)")]
        public static partial Regex GlobalEpRegex();
        [GeneratedRegex("bangumi/media/(md\\d+)")]
        public static partial Regex BangumiMdRegex();
        [GeneratedRegex("window.__INITIAL_STATE__=([\\s\\S].*?);\\(function\\(\\)")]
        public static partial Regex StateRegex();
        [GeneratedRegex("md(\\d+)")]
        public static partial Regex MdRegex();
        [GeneratedRegex("(^|&)?(\\w+)=([^&]+)(&|$)?", RegexOptions.Compiled)]
        public static partial Regex QueryRegex();
        [GeneratedRegex("libavutil\\s+(\\d+)\\. +(\\d+)\\.")]
        public static partial Regex LibavutilRegex();
    }
}
