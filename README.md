# FrameSource
Implementing .NET USB camera frame grabbers

# IFrameSource
simple common interface:

- Live() : Start live capture worker
- Freeze() : Wait for the actual frame (on caller thread) and pause worker
- ShowProperties() : All the UI for live configuration
- CopyFrame(destination) : Safe copy last frame to managed or unmanaged store
- event OnNewFrame : New frame notification
- event OnWorkerException : Any worker exception notification, like camera lost

# MF_LiveFrameSource
implementation with Microsoft MediaFoundation (MFSourceReader) technology: 

Features:
- No software transforms
- Configure frame source reader on the fly
- AM* (DirectShow) properties support
- Full Serialization/Deserialization support
- COM based on SharpDX technonogy
- Fast and CPU effective

Sample:
- WindowsForms USB Camera Live viewer sample with DirectX viewport based on SharpDX 
- CPU loading is the same as Windows 10 "Camera" app
- Camera configuration stored with Json
- FullScreen mode toggle with double click on viewport

<img src="images/Lupas.Samples.FrameSource.jpg" width="50%">

Tests:
- Some unit tests based on NUnit
