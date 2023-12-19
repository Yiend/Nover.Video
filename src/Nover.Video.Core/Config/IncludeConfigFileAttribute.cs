using System;

namespace Nover.Video.Core
{
	/// <summary>
	/// 用于标记包含资源文件的程序集。
	/// </summary>
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	public sealed class IncludeConfigFileAttribute : Attribute
	{
	}
}
