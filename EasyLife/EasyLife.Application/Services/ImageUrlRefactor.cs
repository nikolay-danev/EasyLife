using System;
using System.Collections.Generic;
using System.Text;
using EasyLife.Domain.Models;

namespace EasyLife.Application.Services
{
	public static class ImageUrlRefactor
	{
		public static string RefactorUrl(Advertisement advertisement, string textToReplace)
		{
			return advertisement.ImageUrl.Replace(textToReplace, "");
		}
	}
}
