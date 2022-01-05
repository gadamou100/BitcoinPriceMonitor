﻿using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinPriceMonitor.Application.Interfaces
{
    public interface IJsonParserToPriceSnapshotFactory
    {
        Maybe<IJsonParserToPriceSnapshot> CreateParser(string sourceId);
    }
}
