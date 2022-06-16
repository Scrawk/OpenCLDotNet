using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

using OpenCLDotNet.Buffers;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Core
{
    public static partial class CL
    {

        public static cl_mem CreateImage(
            cl_context context,
            CL_MEM_FLAGS flags,
            CLImageFormat format,
            CLImageDescription desc,
            Array data,
            out CL_ERROR error)
        {
            return CL_CreateImage(context, flags, format, desc, data, out error);
        }

        public static CL_ERROR GetSupportedImageFormatsSize(
            cl_context context,
            CL_MEM_FLAGS flags,
            CL_MEM_OBJECT_TYPE type,
            out uint num_formats)
        {
            cl_uint size;
            var error = CL_GetSupportedImageFormatsSize(context, flags, type, 0, out size);

            num_formats = size;
            return error;
        }

        public static CL_ERROR GetSupportedImageFormats(
            cl_context context,
            CL_MEM_FLAGS flags,
            CL_MEM_OBJECT_TYPE type,
            CLImageFormat[] formats)
        {
            uint num_entries = (uint)formats.Length;
            return CL_GetSupportedImageFormats(context, flags, type, num_entries, formats);
        }

        private static Dictionary<CLImageFormatKey, CLImageFormat[]> FormatTable { get; set; }

        public static void ClearFormatTable()
        {
            FormatTable = null;
        }

        public static void GetSupportedImageFormats(CLImageFormatKey key, List<CLImageFormat> formats)
        {

        }

        public static bool ImageFormatIsSupported(CLImageFormatKey key, CLImageFormat format, out CL_ERROR error)
        {
            if(FormatTable == null || 
               !FormatTable.TryGetValue(key, out CLImageFormat[] formats))
            {
                if(FormatTable == null)
                    FormatTable = new Dictionary<CLImageFormatKey, CLImageFormat[]>();

                var id = key.Context;
                var flags = key.Flags;
                var type = key.MemType;

                error = CL.GetSupportedImageFormatsSize(id, flags, type, out uint size);
                if(error != CL_ERROR.SUCCESS)
                    return false;

                formats = new CLImageFormat[size];
                error = CL.GetSupportedImageFormats(id, flags, type, formats);
                if (error != CL_ERROR.SUCCESS)
                    return false;
                    
                FormatTable.Add(key, formats);
                return formats.Contains(format);
            }
            else
            {
                error = CL_ERROR.SUCCESS;
                return formats.Contains(format);
            }
        }

        public static CL_ERROR GetImageInfoSize(
            cl_mem image,
            CL_IMAGE_INFO name,
            out uint size)
        {
            size_t sizet;
            var err = CL_GetImageInfoSize(image, name, out sizet);
            size = (uint)sizet;
            return err;
        }

        public static CL_ERROR GetImageInfo(
            cl_mem image,
            CL_IMAGE_INFO name,
            size_t size,
            out UInt64 info)
        {
            return CL_GetImageInfo(image, name, size, out info);
        }

        public static CL_ERROR GetImageInfo(
            cl_mem image,
            CL_IMAGE_INFO name,
            size_t size,
            out CLImageFormat info)
        {
            return CL_GetImageInfo(image, name, size, out info);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////
        //                                 EXTERN FUNCTIONS                                          ///
        ///////////////////////////////////////////////////////////////////////////////////////////////

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern cl_mem CL_CreateImage(
            cl_context context,
            CL_MEM_FLAGS flags,
            CLImageFormat format,
            CLImageDescription desc,
            Array data,
            [Out] out CL_ERROR error);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetSupportedImageFormatsSize(
            cl_context context,
            CL_MEM_FLAGS flags,
            CL_MEM_OBJECT_TYPE type,
            cl_uint num_entries,
            [Out] out cl_uint num_formats);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetSupportedImageFormats(
            cl_context context,
            CL_MEM_FLAGS flags,
            CL_MEM_OBJECT_TYPE type,
            cl_uint num_entries,
            [Out] CLImageFormat[] formats);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetImageInfoSize(
            cl_mem image,
            CL_IMAGE_INFO name,
            [Out] out size_t size);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetImageInfo(
            cl_mem image,
            CL_IMAGE_INFO name,
            size_t size,
            [Out] out UInt64 info);

        [DllImport(DLL_NAME, CallingConvention = CDECL)]
        private static extern CL_ERROR CL_GetImageInfo(
            cl_mem image,
            CL_IMAGE_INFO name,
            size_t size,
            [Out] out CLImageFormat info);

    }
}
