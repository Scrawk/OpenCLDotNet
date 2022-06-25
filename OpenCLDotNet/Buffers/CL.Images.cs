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
        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<CLImageFormatKey, CLImageFormat[]> FormatTable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="flags"></param>
        /// <param name="format"></param>
        /// <param name="desc"></param>
        /// <param name="byte_size"></param>
        /// <param name="data">Refers to a valid buffer memory object if image_type is CL_MEM_OBJECT_IMAGE1D_BUFFER. 
        /// Otherwise it must be NULL. For a 1D image buffer object, the image pixels are taken from the buffer 
        /// object's data store. When the contents of a buffer object's data store are modified, those changes 
        /// are reflected in the contents of the 1D image buffer object and vice-versa at corresponding sychronization points.
        /// The image_width * size of element in bytes must be ≤ size of buffer object data store..</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static cl_mem CreateImage(
            cl_context context,
            CL_MEM_FLAGS flags,
            CLImageFormat format,
            CLImageDescription desc,
            uint byte_size,
            Array data,
            out CL_ERROR error)
        {
            return CL_CreateImage(context, flags, format, desc, data, out error);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="flags"></param>
        /// <param name="type"></param>
        /// <param name="num_formats"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="flags"></param>
        /// <param name="type"></param>
        /// <param name="formats"></param>
        /// <returns></returns>
        public static CL_ERROR GetSupportedImageFormats(
            cl_context context,
            CL_MEM_FLAGS flags,
            CL_MEM_OBJECT_TYPE type,
            CLImageFormat[] formats)
        {
            uint num_entries = (uint)formats.Length;
            return CL_GetSupportedImageFormats(context, flags, type, num_entries, formats);
        }

        /// <summary>
        /// 
        /// </summary>
        public static void ClearFormatTable()
        {
            FormatTable = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="formats"></param>
        /// <returns></returns>
        public static CL_ERROR GetSupportedImageFormats(CLImageFormatKey key, List<CLImageFormat> formats)
        {
            CLImageFormat[] formats_array = null;
            var error = AddImageFormat(key, out formats_array);

            if (error == CL_ERROR.SUCCESS)
                formats.AddRange(formats_array);
           
            return error;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="format"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static bool ImageFormatIsSupported(CLImageFormatKey key, CLImageFormat format, out CL_ERROR error)
        {
            CLImageFormat[] formats = null;
            error = AddImageFormat(key, out formats);

            if(error == CL_ERROR.SUCCESS)
            {
                return formats.Contains(format);
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="formats"></param>
        /// <returns></returns>
        private static CL_ERROR AddImageFormat(CLImageFormatKey key, out CLImageFormat[] formats)
        {
            if (FormatTable == null)
                FormatTable = new Dictionary<CLImageFormatKey, CLImageFormat[]>();

            if (!FormatTable.TryGetValue(key, out formats))
            {
                var id = key.Context;
                var flags = key.Flags;
                var type = key.MemType;

                var error = CL.GetSupportedImageFormatsSize(id, flags, type, out uint size);
                if (error != CL_ERROR.SUCCESS)
                {
                    formats = null;
                    return error;
                }
                    
                formats = new CLImageFormat[size];
                error = CL.GetSupportedImageFormats(id, flags, type, formats);
                if (error != CL_ERROR.SUCCESS)
                {
                    formats = null;
                    return error;
                }

                FormatTable.Add(key, formats);
                return CL_ERROR.SUCCESS;
            }
            else
            {
                return CL_ERROR.SUCCESS;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static CL_ERROR GetImageInfo(
            cl_mem image,
            CL_IMAGE_INFO name,
            size_t size,
            out UInt64 info)
        {
            return CL_GetImageInfo(image, name, size, out info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <param name="info"></param>
        /// <returns></returns>
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
