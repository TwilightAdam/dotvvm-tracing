﻿using System;
using DotVVM.Framework.Runtime.Tracing;
using DotVVM.Tracing.MiniProfiler.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Profiling;

namespace DotVVM.Framework.Configuration
{
    public static class MiniProfilerBuilderExtensions
    {
        /// <summary>
        /// Registers MiniProfiler tracer and MiniProfilerWidget
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IDotvvmServiceCollection AddMiniProfilerEventTracing(this IDotvvmServiceCollection services)
        {
            services.Services.AddTransient<IRequestTracer, MiniProfilerTracer>();

            services.Services.Configure((MiniProfilerOptions opt) =>
            {
                opt.IgnoredPaths.Add("/dotvvmResource/");
            });

            services.Services.Configure((DotvvmConfiguration conf) =>
            {
                conf.Markup.AddCodeControls(DotvvmConfiguration.DotvvmControlTagPrefix, typeof(MiniProfilerWidget));
                conf.Runtime.GlobalFilters.Add(new MiniProfilerActionFilter());
            });

            return services;
        }
    }
}