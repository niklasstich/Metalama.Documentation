---
uid: telemetry
level: 200
---

# Telemetry

This article explains the data that Metalama collects and how you can modify its settings.

## What is being collected?

By default, Metalama collects and sends usage and quality reports to PostSharp Technologies. These telemetry reports are anonymous and are collected under the following circumstances:

- In the event of an unexpected _failure_ or _performance degradation_. In such cases, an exception report, inclusive of an anonymized call stack, is reported.
- Periodically, for each project you are building, we gather data such as a one-way hash of the project name, the target framework version, the project size, the number of aspects used, the amount of code saved by Metalama, and performance metrics.

All reports include a randomly generated device id, which you can [reset at any time using Metalama Command Line Tools](#resetting-your-device-id).

Telemetry data is collected and processed in accordance with our [Privacy Policy](https://www.postsharp.net/company/legal/privacy-policy).

### License audit

In addition to telemetry, the software may undergo a _license audit_. This audit is anonymous but mandatory for Metalama Free and the self-generated Metalama Trial. It is used to gather statistics on the number of users. If you are using a license key, license audit reports include the id of your license key. If you disagree with license auditing, please [contact our sales team](mailto:hello@postsharp.net), and we will provide you with a new license key that includes a license audit waiver flag.

## Disabling telemetry

You can disable telemetry using one of the two methods outlined below.

### Option 1. Defining an environment variable

You can disable telemetry by defining the `METALAMA_TELEMETRY_OPT_OUT` environment variable to any non-empty value.

This method allows you to disable telemetry for build agents, or you can disable telemetry for all devices in your domain using remote management tools such as Azure Endpoint Manager.

### Option 2. Using Metalama Command Line Tools

1. Install Metalama Command Line Tools as outlined in <xref:dotnet-tool>.
2. Execute the following commands:

   ```powershell
   metalama telemetry disable
   ```

## Resetting your device id

Metalama Telemetry uses a randomly generated GUID to uniquely identify your device. You can reset this ID at any time. Once you reset your ID, PostSharp Technologies will no longer be able to correlate past and future reports.

1. Install Metalama Command Line Tools as outlined in <xref:dotnet-tool>.
2. Execute the following commands:

   ```powershell
   metalama telemetry reset-device-id
   ```

