﻿using API.Models;
using MediatR;

namespace BooksAPI.Command;

public record UpdateBookCommand(Book book): IRequest;