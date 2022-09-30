using OrderService.Data.IRepositories;
using OrderService.Domain.Entities.Commons;
using OrderService.Service.Exceptions;
using OrderService.Service.Helpers;
using OrderService.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Service.Services;

public class AttachmentService : IAttachmentService
{
    private readonly IUnitOfWork unitOfWork;
    public AttachmentService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async ValueTask<bool> DeleteFileAsync(Expression<Func<Attachment, bool>> expression)
    {
        var attachment =
            await this.unitOfWork.Attachments.GetAsync(expression);

        if (attachment is null)
            throw new CustomException(404, "Couldn't find file for given id");

        await this.unitOfWork.Attachments.DeleteAsync(expression);

        return true;
    }

    public async ValueTask<Attachment> ModifyFileAsync(long id, Stream stream)
    {
        var attachment = 
            await this.unitOfWork.Attachments.GetAsync(attachment => attachment.Id.Equals(id));

        if (attachment is null)
            throw new CustomException(404, "Couldn't find file for given id");

        attachment = await this.unitOfWork.Attachments.UpdateAsync(attachment);

        return attachment;
    }

    public async ValueTask<Attachment> UploadFileAsync(Stream stream, string fileName)
    {
        fileName = Guid.NewGuid().ToString("N") + "-" + fileName;
        var filePath = Path.Combine(EnvironmentHelper.AttachmentPath, fileName);

        var fileStream = File.Create(filePath);
        await fileStream.CopyToAsync(stream);

        await fileStream.FlushAsync();
        fileStream.Close();

        var attachment = await this.unitOfWork.Attachments.CreateAsync(new Attachment()
        {
            Name = Path.GetFileName(filePath),
            Path = Path.Combine(EnvironmentHelper.FilePath, Path.GetFileName(filePath)),
            CreatedAt = DateTime.UtcNow
        });

        return attachment;
    }
}
