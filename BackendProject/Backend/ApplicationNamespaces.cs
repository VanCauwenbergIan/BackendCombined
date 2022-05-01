//.NET
global using System;
global using Microsoft.Extensions.Options;

// Nuget
global using MongoDB.Bson;
global using MongoDB.Bson.Serialization.Attributes;
global using MongoDB.Driver;
global using AutoMapper;
global using FluentValidation;
global using FluentValidation.AspNetCore;
global using Microsoft.IdentityModel.Tokens;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using Microsoft.AspNetCore.Authorization;

// Customer
global using Games.Models;
global using Games.Configuration;
global using Games.Context;
global using Games.Repositories;
global using Games.Services;
global using Games.DTO;
global using Games.Profiles;
global using Games.Validators;
global using Games.GraphQL;