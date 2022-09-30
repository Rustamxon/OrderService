using OrderService.Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Service.Interfaces;

public interface IAttachmentService
{
    ValueTask<Attachment> UploadFileAsync(Stream stream, string fileName);
    ValueTask<Attachment> ModifyFileAsync(long id, Stream stream);
    ValueTask<bool> DeleteFileAsync(Expression<Func<Attachment, bool>> expression);
}
