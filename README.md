# FrameSource

Implementing .NET live camera frame grabbers with simple interface:

- Live()                  //  Start live capture worker
- Freeze()                //  Wait for the actual frame (on caller thread) and pause worker
- ShowProperties()        //  All the UI for live configuration
- CopyFrame(destination)  //  Safe copy last frame to managed or unmanaged store

1. Microsoft MediaFoundation (MFSourceReader) technology: 
class MF_LiveFrameSource

Features:
- No software transforms
- Configure frame reader on the fly
- AM* properties support
- Full Serialization/Deserialization support
- COM based on SharpDX technonogy
- Fast and CPU effective

Sample:
- WindowsForms sample with DirectX viewport based on SharpDX 
- CPU loading is the same as Windows 10 "Camera" app
- Camera configuration stored with Json

Tests:
- Some unit tests based on NUnit
