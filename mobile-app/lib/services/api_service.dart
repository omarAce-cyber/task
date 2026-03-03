import 'dart:convert';
import 'package:http/http.dart' as http;
import '../models/notification_model.dart';

class ApiService {
  static const String baseUrl = 'http://10.0.2.2:5000'; // localhost for Android emulator

  Future<List<NotificationModel>> getNotifications() async {
    final response = await http.get(Uri.parse('$baseUrl/api/notification'));

    if (response.statusCode == 200) {
      final List<dynamic> data = json.decode(response.body);
      return data
          .map((item) => NotificationModel.fromJson(item as Map<String, dynamic>))
          .toList();
    } else {
      throw Exception('Failed to load notifications: ${response.statusCode}');
    }
  }

  Future<void> registerDevice(String deviceToken, String deviceName) async {
    final response = await http.post(
      Uri.parse('$baseUrl/api/device/register'),
      headers: {'Content-Type': 'application/json'},
      body: json.encode({
        'deviceToken': deviceToken,
        'deviceName': deviceName,
      }),
    );

    if (response.statusCode != 200) {
      throw Exception('Failed to register device: ${response.statusCode}');
    }
  }
}
