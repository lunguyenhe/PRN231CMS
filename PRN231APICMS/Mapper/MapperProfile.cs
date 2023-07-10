﻿using AutoMapper;
using PRN231APICMS.Models;

namespace PRN231APICMS.Mapper
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<TestQuestion, TestQuestionDTO>().ForMember(des => des.Content, act =>act.MapFrom(src=>src.Question.Content));
            CreateMap<SubmittedAssignment, SubmitAssignmentDTO>().ForMember(des => des.Email, act => act.MapFrom(src => src.User.Email));
        }
    }
}
