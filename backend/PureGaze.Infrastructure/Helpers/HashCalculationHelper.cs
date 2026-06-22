using System.Buffers;
using System.Buffers.Text;
using System.IO.Hashing;
using System.Text;
using PureGaze.Application.Contracts.Integrations.Hrm;

namespace PureGaze.Infrastructure.Helpers;

public static class HashCalculationHelper
{
    public static ulong CalculateHash(HrmEmployeeDto e)
    {
        byte[] buffer = ArrayPool<byte>.Shared.Rent(4096);
        try
        {
            int pos = 0;

            void WriteString(string? s)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    int written = Encoding.UTF8.GetBytes(s, buffer.AsSpan(pos));
                    pos += written;
                }
                buffer[pos++] = (byte)'|';
            }

            void WriteInt(int? n)
            {
                if (n.HasValue)
                {
                    if (!Utf8Formatter.TryFormat(n.Value, buffer.AsSpan(pos), out int written))
                        return;
                    pos += written;
                }
                buffer[pos++] = (byte)'|';
            }

            void WriteGuid(Guid? g)
            {
                if (g.HasValue)
                {
                    if (!g.Value.TryFormat(buffer.AsSpan(pos), out int written))
                        return;
                    pos += written;
                }
                buffer[pos++] = (byte)'|';
            }

            WriteString(e.FirstNameEn);
            WriteString(e.LastNameEn);
            WriteString(e.Email);
            WriteString(e.LifecycleStatus);

            WriteGuid(e.ProfessionalLevelId);
            WriteGuid(e.ManagerialLevelId);

            WriteInt(e.ManagerId);
            WriteInt(e.HeadId);
            WriteInt(e.RMId);
            WriteInt(e.M1Id);
            WriteInt(e.M2Id);
            WriteInt(e.M3Id);
            WriteInt(e.M4Id);

            return XxHash64.HashToUInt64(buffer.AsSpan(0, pos));
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(buffer);
        }
    }
}