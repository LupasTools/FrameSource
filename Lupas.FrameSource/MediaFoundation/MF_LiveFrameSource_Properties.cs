// Copyright (c) 2020 Aleksei Osipov [Lupas] https://github.com/LupasTools
// MF_LiveFrameSource_Properties.cs : 30.8.2020
// MIT license

using System;
using System.Runtime.InteropServices;

namespace Lupas.FrameSource.MediaFoundation
{
    public class AMCamProperty
    {
        public int ProperyId;
        public string PropertyName;

        internal int Min;
        internal int Max;
        internal int Delta;

        public int Value;
        internal int DefaultValue;

        public int Flag;
        internal int PossibleFlags;

        public override string ToString()
        {
            return $"{PropertyName} {Value} {(VideoProcAmpFlags)Flag}";
        }
    }
    internal class AMVideoProcAmpProperty : AMCamProperty { };
    internal class AMCameraControlProperty : AMCamProperty { };
    
    public enum MediaTransform
    {
        NoTransform,
        ForceNV12,
        ForceRGB32
    }

    #region AM_COMInterfaces
    /// <summary>
    /// The IAMCameraControl interface controls camera settings such as zoom, pan, aperture adjustment,
    /// or shutter speed. To obtain this interface, query the filter that controls the camera.
    /// </summary>
    [ComImport,
    Guid("C6E13370-30AC-11d0-A18C-00A0C9118956"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IAMCameraControl
    {
        /// <summary>
        /// Gets the range and default value of a specified camera property.
        /// </summary>
        /// 
        /// <param name="Property">Specifies the property to query.</param>
        /// <param name="pMin">Receives the minimum value of the property.</param>
        /// <param name="pMax">Receives the maximum value of the property.</param>
        /// <param name="pSteppingDelta">Receives the step size for the property.</param>
        /// <param name="pDefault">Receives the default value of the property. </param>
        /// <param name="pCapsFlags">Receives a member of the CameraControlFlags enumeration, indicating whether the property is controlled automatically or manually.</param>
        /// 
        /// <returns>Return's <b>HRESULT</b> error code.</returns>
        /// 
        [PreserveSig]
        int GetRange(
            [In] CameraControlProperty Property,
            [Out] out int pMin,
            [Out] out int pMax,
            [Out] out int pSteppingDelta,
            [Out] out int pDefault,
            [Out] out CameraControlFlags pCapsFlags
            );

        /// <summary>
        /// Sets a specified property on the camera.
        /// </summary>
        /// 
        /// <param name="Property">Specifies the property to set.</param>
        /// <param name="lValue">Specifies the new value of the property.</param>
        /// <param name="Flags">Specifies the desired control setting, as a member of the CameraControlFlags enumeration.</param>
        /// 
        /// <returns>Return's <b>HRESULT</b> error code.</returns>
        /// 
        [PreserveSig]
        int Set(
            [In] CameraControlProperty Property,
            [In] int lValue,
            [In] CameraControlFlags Flags
            );

        /// <summary>
        /// Gets the current setting of a camera property.
        /// </summary>
        /// 
        /// <param name="Property">Specifies the property to retrieve.</param>
        /// <param name="lValue">Receives the value of the property.</param>
        /// <param name="Flags">Receives a member of the CameraControlFlags enumeration.
        /// The returned value indicates whether the setting is controlled manually or automatically.</param>
        /// 
        /// <returns>Return's <b>HRESULT</b> error code.</returns>
        /// 
        [PreserveSig]
        int Get(
            [In] CameraControlProperty Property,
            [Out] out int lValue,
            [Out] out CameraControlFlags Flags
            );
    }

    /// <summary>
    /// The IAMVideoProcAmp interface controls video camera settings such as brightness, contrast, hue,
    /// or saturation. To obtain this interface, cast the MediaSource.
    /// </summary>
    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
        Guid("C6E13360-30AC-11D0-A18C-00A0C9118956"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IAMVideoProcAmp
    {
        /// <summary>
        /// Get the range and default value of a camera property.
        /// </summary>
        ///
        /// <param name="Property">The property.</param>
        /// <param name="pMin">The min value.</param>
        /// <param name="pMax">The max value.</param>
        /// <param name="pSteppingDelta">The step size.</param>
        /// <param name="pDefault">The deafult value. </param>
        /// <param name="pCapsFlags">Shows if it can be controlled automatically and/or manually.</param>
        ///
        /// <returns>Error code.</returns>
        ///
        [PreserveSig]
        int GetRange(
            [In] VideoProcAmpProperty Property,
            [Out] out int pMin,
            [Out] out int pMax,
            [Out] out int pSteppingDelta,
            [Out] out int pDefault,
            [Out] out VideoProcAmpFlags pCapsFlags
            );

        /// <summary>
        /// Set a specified property on the camera.
        /// </summary>
        ///
        /// <param name="Property">The property to set.</param>
        /// <param name="lValue">The new value of the property.</param>
        /// <param name="Flags">The auto or manual setting.</param>
        ///
        /// <returns>Error code.</returns>
        ///
        [PreserveSig]
        int Set(
            [In] VideoProcAmpProperty Property,
            [In] int lValue,
            [In] VideoProcAmpFlags Flags
            );

        /// <summary>
        /// Get the current setting of a camera property.
        /// </summary>
        ///
        /// <param name="Property">The property to retrieve.</param>
        /// <param name="lValue">The current value of the property.</param>
        /// <param name="Flags">Is it manual or automatic?</param>
        ///
        /// <returns>Error code.</returns>
        ///
        [PreserveSig]
        int Get(
            [In] VideoProcAmpProperty Property,
            [Out] out int lValue,
            [Out] out VideoProcAmpFlags Flags
            );
    }
    /// <summary>
    /// The list of camera property settings
    /// </summary>
    internal enum CameraControlProperty
    {
        Pan = 0,
        Tilt,
        Roll,
        Zoom,
        Exposure,
        Iris,
        Focus,
        KSPROPERTY_CAMERACONTROL_SCANMODE = 7,
        KSPROPERTY_CAMERACONTROL_PRIVACY = 8,
        KSPROPERTY_CAMERACONTROL_PANTILT = 9,
        KSPROPERTY_CAMERACONTROL_PAN_RELATIVE = 10,
        KSPROPERTY_CAMERACONTROL_TILT_RELATIVE = 11,
        KSPROPERTY_CAMERACONTROL_ROLL_RELATIVE = 12,
        KSPROPERTY_CAMERACONTROL_ZOOM_RELATIVE = 13,
        KSPROPERTY_CAMERACONTROL_EXPOSURE_RELATIVE = 14,
        KSPROPERTY_CAMERACONTROL_IRIS_RELATIVE = 15,
        KSPROPERTY_CAMERACONTROL_FOCUS_RELATIVE = 16,
        KSPROPERTY_CAMERACONTROL_PANTILT_RELATIVE = 17,
        KSPROPERTY_CAMERACONTROL_FOCAL_LENGTH = 18,
        LightCompensation = 19
    }

    /// <summary>
    /// Is the setting automatic?
    /// </summary>
    [Flags]
    internal enum CameraControlFlags
    {
        None = 0x0,
        Auto = 0x0001,
        Manual = 0x0002
    }
    /// <summary>
    /// The list of video camera settings
    /// </summary>
    internal enum VideoProcAmpProperty
    {
        Brightness = 0,
        Contrast,
        Hue,
        Saturation,
        Sharpness,
        Gamma,
        ColorEnable,
        WhiteBalance,
        BacklightCompensation,
        Gain
    }

    /// <summary>
    /// The auto and manual flag
    /// </summary>
    [Flags]
    internal enum VideoProcAmpFlags
    {
        None = 0x0,
        Auto = 0x0001,
        Manual = 0x0002
    }
    #endregion
}