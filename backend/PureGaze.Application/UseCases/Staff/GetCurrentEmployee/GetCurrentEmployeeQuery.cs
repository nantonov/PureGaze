using System.Text.Json.Serialization;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Staff.GetCurrentEmployee;

public sealed record GetCurrentEmployeeQuery(string Email) 
    : IRequest<GetCurrentEmployeeResponse>;