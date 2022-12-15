
Imports System

Namespace Blake2sCSharp
    ' from the ref C, since we are not doing any tree hashing, we care about '+' of the following:
    ' typedef struct __blake2s_param
    ' {
    ' uint8_t  digest_length; // 1 +
    ' uint8_t  key_length;    // 2 +
    ' uint8_t  fanout;        // 3 =1
    ' uint8_t  depth;         // 4 =1
    ' uint32_t leaf_length;   // 8 =0
    ' uint8_t  node_offset[6];// 14 =0
    ' uint8_t  node_depth;    // 15 =0
    ' uint8_t  inner_length;  // 16 =0
    ' uint8_t  salt[BLAKE2S_SALTBYTES]; // 24 +
    ' uint8_t  personal[BLAKE2S_PERSONALBYTES];  // 32 +
    ' } blake2s_param;
    ' //

    Friend Module Blake2sIvBuilder
        Public Function ConfigS(ByVal config As Blake2sConfig) As UInteger()

            Dim rawConfig = New UInteger(7) {} '
            'var result = new UInt32[8]; //

            'digest length
            If config.OutputSizeInBytes <= 0 Or config.OutputSizeInBytes > 32 Then Throw New ArgumentOutOfRangeException("config.OutputSize") '
            rawConfig(0) = rawConfig(0) Or CUInt(config.OutputSizeInBytes) '

            'Key length
            If config.Key IsNot Nothing Then
                If config.Key.Length > 32 Then Throw New ArgumentException("config.Key", "Key too long") '
                rawConfig(0) = rawConfig(0) Or CUInt(config.Key.Length << 8) '
            End If
            ' Fan Out =1 and Max Height / Depth = 1
            rawConfig(0) = rawConfig(0) Or 1 << 16
            rawConfig(0) = rawConfig(0) Or 1 << 24
            ' Leaf Length and Inner Length 0, no need to worry about them
            ' Salt
            If config.Salt IsNot Nothing Then
                If config.Salt.Length <> 8 Then Throw New ArgumentException("config.Salt has invalid length")
                rawConfig(4) = Blake2sCore.BytesToUInt32(config.Salt, 0)
                rawConfig(5) = Blake2sCore.BytesToUInt32(config.Salt, 4)
            End If
            ' Personalization
            If config.Personalization IsNot Nothing Then
                If config.Personalization.Length <> 8 Then Throw New ArgumentException("config.Personalization has invalid length")
                rawConfig(6) = Blake2sCore.BytesToUInt32(config.Personalization, 0)
                rawConfig(7) = Blake2sCore.BytesToUInt32(config.Personalization, 4)
            End If

            Return rawConfig
        End Function

    End Module
End Namespace