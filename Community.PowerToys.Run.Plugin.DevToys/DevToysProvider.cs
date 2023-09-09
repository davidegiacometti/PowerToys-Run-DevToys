// Copyright (c) Davide Giacometti. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Windows.Management.Deployment;

namespace Community.PowerToys.Run.Plugin.DevToys
{
    public class DevToysProvider
    {
        private readonly PackageManager _packageManager;

        private readonly List<Utility> _utilities = new List<Utility>
        {
            new Utility("base64", "Base64 Text Encoder / Decoder"),
            new Utility("base64img", "Base64 Image Encoder / Decoder"),
            new Utility("baseconverter", "Number Base Converter"),
            new Utility("certficate", "Certificate Decoder"),
            new Utility("checksum", "Checksum Generator"),
            new Utility("color", "Color Picker & Contrast"),
            new Utility("colorblind", "Color Blindness Simulator"),
            new Utility("cronparser", "Cron expression parser"),
            new Utility("diff", "Text Comparer"),
            new Utility("escape", "Text Escape / Unescape"),
            new Utility("gzip", "GZip Compress / Decompress"),
            new Utility("hash", "Hash Generator"),
            new Utility("html", "HTML Encoder / Decoder"),
            new Utility("imageconverter", "Image Converter"),
            new Utility("imgcomp", "PNG / JPEG Compressor"),
            new Utility("jsonformat", "Json Formatter"),
            new Utility("jsonyaml", "JSON <> YAML Converter"),
            new Utility("jwt", "Encoder / Decoder"),
            new Utility("loremipsum", "Lorem Ipsum Generator"),
            new Utility("markdown", "Markdown Preview"),
            new Utility("password", "Password Generator"),
            new Utility("regex", "Regex Tester"),
            new Utility("sqlformat", "SQL Formatter"),
            new Utility("string", "Text Case Converter and Inspector"),
            new Utility("time", "Unix Timestamp Converter"),
            new Utility("url", "URL Encoder / Decoder"),
            new Utility("uuid", "UUID Generator"),
            new Utility("xmlformat", "XML Formatter"),
            new Utility("xmlvalidator", "XML Validator"),
        };

        private ImageSource? _logo;

        public ReadOnlyCollection<Utility> Utilities => _utilities.AsReadOnly();

        public ImageSource? Logo => _logo;

        public DevToysProvider()
        {
            _packageManager = new PackageManager();
        }

        public bool FindDevToys()
        {
            var user = WindowsIdentity.GetCurrent().User;
            if (user != null)
            {
                var package = _packageManager.FindPackagesForUser(user.Value).SingleOrDefault(p => p.Id.Name.Equals("64360VelerSoftware.DevToys", StringComparison.Ordinal));
                if (package != null)
                {
                    var bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.UriSource = package.Logo;
                    bitmapImage.EndInit();
                    bitmapImage.Freeze();
                    _logo = bitmapImage;
                    return true;
                }
            }

            return false;
        }

        public record Utility(string Protocol, string Name);
    }
}
