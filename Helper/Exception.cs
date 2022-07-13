﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace KMS.Helper
{
    public static class ExceptionHelper
    {
        public static Exception SetCode(this Exception e, int value)
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo fieldInfo = typeof(Exception).GetField("_HResult", flags);

            fieldInfo.SetValue(e, value);

            return e;
        }
    }
}