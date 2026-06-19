using BusTicketManagement.Application.Services;
using BusTicketManagement.Application.Interfaces;
using BusTicketManagement.Infrastructure.Data;
using BusTicketManagement.Infrastructure.Repository;
using AutoMapper;
using BusTicketManagement.Application.DTOs.BusDTOs;
using BusTicketManagement.Domain.Entities;

namespace BusTicketManagement.API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Bus Mappings
        CreateMap<Bus, BusDto>().ReverseMap();
        CreateMap<CreateBusDto, Bus>();

        // Route Mappings
        CreateMap<Route, RouteDTOs.RouteDto>().ReverseMap();
        CreateMap<RouteDTOs.CreateRouteDto, Route>();

        // Booking Mappings
        CreateMap<Booking, BookingDTOs.BookingDto>().ReverseMap();
        CreateMap<BookingDTOs.CreateBookingDto, Booking>();

        // Schedule Mappings
        CreateMap<Schedule, ScheduleDTOs.ScheduleDto>().ReverseMap();
        CreateMap<ScheduleDTOs.CreateScheduleDto, Schedule>();

        // Payment Mappings
        CreateMap<Payment, PaymentDTOs.PaymentDto>().ReverseMap();
        CreateMap<PaymentDTOs.CreatePaymentDto, Payment>();

        // Chat Mappings
        CreateMap<ChatMessage, ChatDTOs.ChatMessageDto>().ReverseMap();
        CreateMap<ChatDTOs.SendChatMessageDto, ChatMessage>();
    }
}
